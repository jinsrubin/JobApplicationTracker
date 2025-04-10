import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobApplicationRequest } from './models/job-application-request.model';
import { JobApplicationResponse } from './models/job-application-response.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class JobApplicationsService {

  constructor(private http: HttpClient) { }

  getAllJobApplications(): Observable<JobApplicationResponse[]> {
    return this.http.get<JobApplicationResponse[]>('/api/jobapplications');
  }
  getJobApplicationById(id: number): Observable<JobApplicationResponse> {
    return this.http.get<JobApplicationResponse>(`/api/jobapplications/${id}`);
  }
  addJobApplication(jobApplication: JobApplicationRequest): Observable<JobApplicationResponse> {
    return this.http.post<JobApplicationResponse>('/api/jobapplications', jobApplication);
  }
  updateJobApplication(id: number, jobApplication: JobApplicationRequest): Observable<JobApplicationResponse> {
    return this.http.put<JobApplicationResponse>(`/api/jobapplications/${id}`, jobApplication);
  }
}
