import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { formatDate } from '@angular/common';
import { JobApplicationsService } from './job-applications.service';
import { JobApplicationRequest } from './models/job-application-request.model';
import { JobApplicationResponse } from './models/job-application-response.model';
import { JobStatus } from './models/job-status';
import { of, catchError, retry } from 'rxjs';
import { retryWhen, delay, scan } from 'rxjs/operators';

@Component({
  selector: 'app-job-applications',
  templateUrl: './job-applications.component.html',
  styleUrl: './job-applications.component.css'
})
export class JobApplicationsComponent implements OnInit {
  jobApplications: JobApplicationResponse[] = [];
  loading: boolean = false; // Loading indicator
  currentPage: number = 1;
  pageSize: number = 5;
  newJobApplication: JobApplicationRequest = {
    companyName: '',
    position: '',
    status: JobStatus.Applied,
    dateApplied: formatDate(new Date(), 'yyyy-MM-dd', 'en')
  };
  editingJobApplication: JobApplicationResponse | null = null;
  statuses = [
    { value: JobStatus.Applied, label: 'Applied' },
    { value: JobStatus.Interview, label: 'Interview' },
    { value: JobStatus.Offer, label: 'Offer' },
    { value: JobStatus.Rejected, label: 'Rejected' }
  ]
  jobStatuses = Object.keys(JobStatus).filter(key => isNaN(Number(key)));

  getStatusLabel(statusValue: number): string {
    const found = this.statuses.find(s => s.value === statusValue);
    return found ? found.label : 'Unknown';
  }

  constructor(private jobApplicationsService: JobApplicationsService,
    private toastr: ToastrService
  ) { }
  ngOnInit(): void {
    this.loadJobApplications();
  }
  InitializeNewApplication(): JobApplicationRequest {
    return {
      companyName: '',
      position: '',
      status: JobStatus.Applied,
      dateApplied: formatDate(new Date(), 'yyyy-MM-dd', 'en')
    };
  }
  loadJobApplications(): void {
    this.loading = true; // Set loading to true before making the API call

    this.jobApplicationsService.getAllJobApplications().pipe(
      retryWhen(errors =>
        errors.pipe(
          scan((acc, error) => {
            if (acc >= 5) {
              throw error; // Rethrow the error after 5 retries
            }
            console.warn(`Retrying... (${acc + 1})`);
            return acc + 1;
          }, 0),
          delay(2000) // Delay between retries
        )
      ),
      catchError((error) => {
        console.error('Error loading job applications', error);
        this.loading = false; // Set loading to false in case of error
        return of([]); // Return an empty array in case of error)
      })
    ).subscribe(
      (data) => {
      this.jobApplications = data;
      this.loading = false; // Set loading to false after receiving the response
      },
      (error) => {
        console.error('Final error after retries', error);
        this.loading = false; // Set loading to false in case of error
      }
    );
  }
  addJobApplication(jobApplicationForm: NgForm): void {
    console.log('Data before sending to API:', this.newJobApplication);

    this.jobApplicationsService.addJobApplication(this.newJobApplication).subscribe(
      (response) => {
        this.toastr.success('Job application added successfully!', 'Success');
        this.jobApplications.push(response);

        this.newJobApplication = {
          companyName: '',
          position: '',
          status: JobStatus.Applied,
          dateApplied: formatDate(new Date(), 'yyyy-MM-dd', 'en')
        };
        // Reset the form after successful submission
        jobApplicationForm.resetForm(this.newJobApplication);
      },
      (error) => {
        if (error.status === 409) {
          this.toastr.error(error.error.message || 'Job application already exists!');
        } else {
          this.toastr.error('Failed to add job application', 'Error');
        }
      }
    );
  }

  getTodayDate(): string {
    const today = new Date();
    return today.toISOString().split('T')[0]; // Format YYYY-MM-DD
  }
  editJobApplication(jobApplication: JobApplicationResponse): void {
    this.editingJobApplication = {
      ...jobApplication,
      dateApplied: formatDate(jobApplication.dateApplied, 'yyyy-MM-dd', 'en')
    };
  }

  updateJobApplication(): void {
    if (this.editingJobApplication) {
      const updateRequest: JobApplicationRequest = {
        companyName: this.editingJobApplication.companyName,
        position: this.editingJobApplication.position,
        status: this.editingJobApplication.status,
        dateApplied: this.editingJobApplication.dateApplied
      }

      this.jobApplicationsService.updateJobApplication(this.editingJobApplication.id, updateRequest).subscribe(() => {
        this.loadJobApplications();
        this.editingJobApplication = null;
        this.toastr.success('Job application updated successfully!', 'Success');
      },
        (error) => {
          if (error.status === 409) {
            this.toastr.error(error.error.message || 'Another Job application with same details exists!');
          } else {
            this.toastr.error('Failed to update job application', 'Error');
          }
          }
      );
    }
  }

  get paginatedJobApplications() {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.jobApplications.slice(startIndex, startIndex + this.pageSize);
  }

  // Pagination buttons
  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  nextPage() {
    if (this.currentPage * this.pageSize < this.jobApplications.length) {
      this.currentPage++;
    }
  }

  get totalPages(): number {
    return Math.ceil(this.jobApplications.length / this.pageSize);
  }

    cancelEdit(): void {
      this.editingJobApplication = null;
    }
  }
