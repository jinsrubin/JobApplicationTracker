# Job Application Tracker
This is a Full Stack ASP.NET Core Web API with Angular project for tracking job applications.

## Project Overview
<p>The Job Application Tracker allows users to:<br>
Add new job applications<br>
View all job applications<br>
Edit/update existing applications (like changing company, position, status to Interview, Offer, or Rejected and date applied)<br>
See all applications in a paginated table<br>
Handle validation and avoid duplicate entries</p>

## Technologies Used
### Backend:
<p>ASP.NET Core Web API (.NET 8)<br>
Entity Framework Core (SQLite database)<br>
Repository Pattern + Dependency Injection<br>
AutoMapper<br>
Swagger (for API documentation)</p>

### Frontend:
<p>Angular 17<br>
HttpClient (for API calls)<br>
ngx-toastr (for toast notifications)<br>
Responsive CSS (Vanilla CSS)</p>

## How to Run
<p>Run Backend + Frontend Together in Visual Studio 2022:<br>
This project was created following the official Microsoft guide:<br>
https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-angular </p>

### Setting up the Startup Project:
<p>Right-click on the Solution → Click Properties.<br>
Under Common Properties > Startup Project, set to Multiple Startup Projects.<br>
For both the Server (ASP.NET Core API) and ClientApp (Angular), set the Action to Start.</p>

###  Run the Application:<br>
<p>Press F5 or click the Start button in Visual Studio.<br>
Two command prompts will open:<br>
One for the ASP.NET Core API.<br>
One for the Angular CLI</p>

### Access the Application:<br>
<p>After a few seconds, the Job Application Tracker form will open in your browser.</p>

### Access Swagger UI<br>
<p>Swagger UI:<br>
https://localhost:port/swagger/index.html<br>

Database will auto-create via EF Core Code-First.<br>
No manual setup needed for SQLite.</p>

## Features
<p> Add New Job: Fill company name, position, status, date applied. Prevents adding duplicate applications.<br>
View Jobs: Paginated table with Company Name, Position, Status, Date Applied, Actions.<br>
Edit Job: Modify Company Name, Position, Status, Date Applied. Validation handled properly.<br>
Pagination: 5 applications per page with Previous/Next buttons.<br>
Toast Notifications: Success, error, and duplicate alerts.<br>
Input Validation: Frontend required fields + backend model validation.<br>
Error Handling: Proper API error handling and retry logic for network failures.<br>
Responsive UI: Works neatly on desktop and shrinks gracefully on small screens.<br>
Async/Await usage: Use async/await throughout the backend (service, repository, and controller layers) to ensure non-blocking, efficient API operations and better scalability.
Global exception handling in the backend: - In Development: Detailed error pages with `UseDeveloperExceptionPage`. In Production: All unhandled exceptions are routed to a centralized `/error` endpoint via `ErrorController`, returning structured `ProblemDetails` responses.<br>
Centralized exception handling: Logging via log4net.</p>

## Assumptions Made
<p>Duplicate checking is based on a combination of Company Name, Position, Status, and Date Applied.<br>
SQLite is used for persistence, but the project could easily swap databases.<br>
Frontend is intentionally kept simple and clean without overcomplicated styling frameworks (no Bootstrap or Material).<br>
Pagination is handled frontend-side (server-side pagination can be added easily later).<br>
Error messages for duplicates or failed operations are user-friendly.<br>
Hardcoded the JobStatus enum inside job-applications.component.ts to keep the technical exercise simple and self-contained, instead of importing it from job-status.ts, which would be preferred in a production-scale project for better maintainability.</p>

## Further Improvements
<p>Add authentication (e.g., login system using JWT).<br>
Implement server-side pagination for large datasets.<br>
Introduce global exception handling middleware in ASP.NET Core.<br>
Write full unit tests and integration tests for both backend and frontend.<br>
Add sorting (click table headers to sort by Company, Position, etc.).<br>
Improve UI/UX with a design framework (e.g., Bootstrap 5 or Angular Material).<br>
Create Dockerfile for backend + frontend deployment.</p>

