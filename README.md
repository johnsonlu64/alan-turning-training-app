# Alan Turing Training App

## Feature: Exercises

**User Story 1:** As a user, I want to be able to choose from a variety of provided exercises, that way I can build routines for my athletes.

**Acceptance Criteria:**
- Can open a page with a list of exercises.
- All exercises are derived from the Supabase backend.
- By default, a variety of common exercises already populate the Supabase table.

**User Story 2:** As a user, I want to be able to create exercises, that way I can add any exercises specialized to our trainings.

**Acceptance Criteria:**
- The "Add Exercise" page offers the button to "Create Exercise".
- Create Exercise opens a page with the options to fill in:
  - Name
  - Type
  - Upload video or image

---

## Feature: Routine Builder

**User Story 1:** As a user, I want to be able to create a new routine from scratch, that way I can create new routines for my athletes.

**Acceptance Criteria:**
- Under class/routines, there is a button to "Create Routine".
- That button opens the Routine Builder.

**User Story 2:** As a user, I want to be able to add exercises to my routine, that way I can create the workout for my athletes to follow.

**Acceptance Criteria:**
- The left panel containing all of the exercises includes a button "Add Exercise".
- Pressing the button opens the "Exercises" page where the user can select one or many using a checkbox selector. They then press "Add Exercise/s" to add them to the working routine.

**User Story 3:** As a user, I want to be able to reorder exercises, that way I can align the routine as needed without having to add or delete to get the right order.

**Acceptance Criteria:**
- Exercises contain arrows to reorder them.

**User Story 4:** As a user, I can enter and use equations for determining set and rep weight for exercises in my routine.

---

## Feature: Reports

**User Story 1:** As a user, I can create reports based on my athlete's data, that way I can easily see my athletes' performance.

**Acceptance Criteria:**
- Opening the Reports page shows an input form.
- The form gives options for how to create the report.
- Pressing create form returns a document.

**User Story 2:** As a user, I can export reports into a CSV file, that way I can easily access everything in one view.

**Acceptance Criteria:**
- All the data is put into a CSV.

**User Story 3:** As a user, I can generate a report based on a custom date range.

**Acceptance Criteria:**
- The bottom of the Reports page allows you to select a date range.

**User Story 4:** As a user, I want to be able to filter the reports by class, sport, or individuals.

**Acceptance Criteria:**
- The Reports page will give you options on how you want it to be filtered.

**User Story 5:** As a user, when I run reports, I want to be able to see a summary of the completion percentage and each student's completion rate.

**Acceptance Criteria:**
- When you run the reports, it'll open a page with a quick summary of everyone's metrics.

---

## Feature: Leaderboard UI Design

**User Story:** As a coach, I want to view athletes ranked on the leaderboard and filter them by class so that I can compare athletes' performance within each class.

**Acceptance Criteria:**
1. The leaderboard displays each athlete's name, performance score, and rank.
2. Selecting a class displays only athletes belonging to that class.
3. Athletes appear in order from highest to lowest score.

---

## Feature: SQL Database Connectivity for Leaderboard

**User Story:** As a coach, I want the leaderboard to use current athlete data from the database so that rankings reflect their recorded performance.

**Acceptance Criteria:**
- The leaderboard retrieves athletes, class memberships, personal records, and body weights from Supabase.
- SQL calculates each athlete's score using the agreed equation: personal-record weight divided by body weight.
- Results are filtered by the selected class and sorted from highest to lowest score.

---

## Feature: Add Students

**User Story:** As a coach, I can add students to my group or class in bulk using a CSV, that way I can import all my students quickly.

**Acceptance Criteria:**
- On the group page or class page, when opening "Add Students" there is an input for a CSV file.
- When uploading the file, you can see success or failure.
- When pressing "Add All", a loop goes through students and adds them into Supabase with the associated class/group ID.

---

## Feature: Notifications

**User Story:** As an instructor, I can see notifications for events for my athletes, that way I can stay up to date with recent events.

**Acceptance Criteria:**
- Create a table "notification" that has `user_id`, `group_id`, and `message` columns.
- Global notification to all users in the class if a student hits a PR.
- Notification to only the athlete if they missed a workout.

---

## Feature: Theme Settings

**User Story:** As a coach, I should be able to change my theme, so that I can change the theme system to my preference.

**Acceptance Criteria:**
1. The settings page provides light, dark, and system theme options.
2. Selecting a theme updates the app's appearance immediately.
3. The selected theme applies consistently across all pages.
4. The app saves the selected theme and restores it when reopened.

---

## Feature: Metrics Data

**User Story:** As an instructor, I want to see each athlete's up-to-date high-level data on the Metrics page.

**Acceptance Criteria:**
- Data organized at a high level from the database.
- High-level data pulled from Supabase and put on the Metrics page.
- Each athlete's data is put on their own personalized Metrics page.

---

## Feature: Profile Picture

**User Story:** As an instructor, I want the possibility of seeing a photo of my athletes if they choose to add a picture.

**Acceptance Criteria:**
- Ability to remove or upload a photo in settings.
- Profile picture shown on each athlete's Metrics page.
- Each athlete's profile photo is shown on the Members page.
- A default photo is provided to athletes and coaches who choose not to add a picture.

---

## Feature: Member Search Bar

**User Story:** As an instructor, I want to be able to search through my members to view their metrics.

**Acceptance Criteria:**
- Navigate to the Members class page.
- Type in a student's name in the search bar.
- The member pops up where the instructor can click on the member.

---

## Additional Changes

- Fixed some metrics navigation.
- Added a navbar.

---

## Team Reflections

**Luke:** Luke enjoyed doing the routine builder very much. Luke also worked on the classes. :) :)

**Aadi:** Aadi had a lot of fun with the settings and leaderboard and learned a lot! As well as students and SQL, and it was educational. :) :)

**Bakary:** Bakary thinks the reports page was very challenging but very rewarding when I finally completed it. :) ;()

**Dasha:** Dasha did the member search bar and integrated the profile pictures of the athletes, and it was very insightful and educational. :) :)