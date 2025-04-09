import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JobApplicationsService } from './job-applications.service';
import { JobApplicationRequest } from './models/job-application-request.model';
import { JobApplicationResponse } from './models/job-application-response.model';
import { JobStatus } from './models/job-status';

@Component({
  selector: 'app-job-applications',
  templateUrl: './job-applications.component.html',
  styleUrl: './job-applications.component.css'
})
export class JobApplicationsComponent implements OnInit {
  jobApplications: JobApplicationResponse[] = [];
  newJobApplication: JobApplicationRequest = {
    companyName: '',
    position: '',
    status: JobStatus.Applied,
    dateApplied: new Date()
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
      dateApplied: new Date()
    };
  }
  loadJobApplications(): void {
    this.jobApplicationsService.getAllJobApplications().subscribe(
      (data) => {
        this.jobApplications = data;
      },
      (error) => {
        console.error('Error loading job applications', error);
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
          dateApplied: new Date()
        };
        // Reset the form after successful submission
        jobApplicationForm.resetForm(this.newJobApplication);
      },
      (error) => {
        console.error('Error adding job application', error);
        this.toastr.error('Failed to add job application', 'Error');
      }
    );
  }

  getTodayDate(): string {
    const today = new Date();
    return today.toISOString().split('T')[0]; // Format YYYY-MM-DD
  }
  editJobApplication(jobApplication: JobApplicationResponse): void {
    this.editingJobApplication = { ...jobApplication };
  }

  saveJobApplication(): void {
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
        }
      );
    }
  }
    cancelEdit(): void {
      this.editingJobApplication = null;
    }
  }
