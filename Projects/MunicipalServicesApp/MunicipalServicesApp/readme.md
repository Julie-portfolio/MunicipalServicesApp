# Municipal Services Application — Part 1: Report Issues

## What this is
A C# .NET Framework (v4.7.2) Windows Forms application that lets residents
report municipal issues (potholes, sanitation, water, etc.) with a location,
category, description, and an optional attached photo/document.

## How to compile and run
1. Open **MunicipalServicesApp.sln** in Visual Studio 2019/2022.
2. Let Visual Studio restore/load the project (no NuGet packages required —
   it only uses the built-in .NET Framework libraries).
3. Press **F5** (or Build → Build Solution, then Debug → Start Without
   Debugging) to compile and run.
4. The Main Menu will open first.

## How to use it
1. From the Main Menu, click **Report Issues** (the other two options are
   disabled for this part of the project — they'll be enabled in Parts 2
   and 3).
2. Fill in:
   - **Location** — where the issue is.
   - **Category** — pick from the dropdown (Sanitation, Roads and
     Potholes, Water and Utilities, etc.).
   - **Description** — details about the issue.
   - **Attach Photo/Document** (optional) — opens a file dialog to attach
     supporting evidence.
3. Watch the **progress bar and message** update as you fill in each field
   — it's designed to encourage residents to complete their report fully.
4. Click **Submit**. If a required field is missing, you'll get a warning;
   otherwise you'll see a confirmation message with a reference number, and
   the issue is stored in the application's in-memory list of reports.
5. Click **Back to Main Menu** to return.

## Project structure
```
MunicipalServicesApp/
├── Program.cs                  # application entry point
├── Models/
│   ├── Issue.cs                 # data model for a reported issue
│   └── IssueRepository.cs       # in-memory List<Issue> store
└── Forms/
    ├── MainForm.cs / .Designer.cs / .resx        # main menu
    └── ReportIssueForm.cs / .Designer.cs / .resx  # report issue screen
```

## Notes for markers
- Reported issues are stored in a `List<Issue>` (see `IssueRepository.cs`),
  as required by the technical specification.
- The engagement feature is a ProgressBar + Label combination on the
  Report Issues form that fills in and updates its message as the resident
  completes each field, reflecting the "real-time feedback" user
  engagement strategy chosen and justified in the Task 1 research
  document.
