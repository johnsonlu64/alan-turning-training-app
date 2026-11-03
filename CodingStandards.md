# SQL Coding Standards

### 1. SQL Naming Convention – Lowercase and snake_case (Your Initials)

**Naming:** // Use lowercase letters and underscores when naming SQL tables and columns. Avoid camelCase and spaces in identifiers.

**Bad:**
```sql
CREATE TABLE StudentRecords (
    StudentID INTEGER,
    FirstName VARCHAR(50)
);
```

**Good:**
```sql
CREATE TABLE student_records (
    student_id INTEGER,
    first_name VARCHAR(50)
);
```

### 2. SQL Keywords – Use Uppercase (Your Initials)

**Capitalization:** // SQL reserved keywords must always be written in uppercase, such as SELECT, FROM, WHERE, INSERT, UPDATE, and DELETE.

**Bad:**
```sql
select first_name
from students
where student_id = 1;
```

**Good:**
```sql
SELECT first_name
FROM students
WHERE student_id = 1;
```

### 3. SQL Indentation – Use Four Spaces (Your Initials)

**Indentation:** // Use four spaces to indent SQL statements when organizing table definitions and longer queries. Keep related statements organized and easy to read.

**Bad:**
```sql
CREATE TABLE students (
student_id INTEGER,
first_name VARCHAR(50),
last_name VARCHAR(50)
);
```

**Good:**
```sql
CREATE TABLE students (
    student_id INTEGER,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
```

### 4. SQL Spacing – Use Consistent Spacing (Your Initials)

**Spacing:** // Always include a space before and after comparison operators (=, >, <), and after commas when separating columns or values.

**Bad:**
```sql
SELECT first_name,last_name
FROM students
WHERE student_id=1;
```

**Good:**
```sql
SELECT first_name, last_name
FROM students
WHERE student_id = 1;
```


### DMC - Space indentation () 


Spacing: // Use 4 spaces for indentation when writing code. 


Bad: //   

        public App() 
          { 
        InitializeComponent(); 
          // The app is designed dark 

         UserAppTheme = AppTheme.Dark; 

        } 


Good: // 
    
    public App() 
    { 
        InitializeComponent(); 

        // The app is designed dark 
        UserAppTheme = AppTheme.Dark; 
    } 
    

 
### DMC - Use PascalCase for naming classes, methods, enumerations, public fields, and public properties.  

Naming: // Capitalize every word. 


Bad: // homePage, homepage, Homepage 


Good :// HomePage 

 

### DMC - Namespace ‘using’ declarations go at the top of the file before any namespaces. Organize the ‘using’ imports by alphabetical order and have System imports always organized first. 

Organize: // declarations at the top in alphabetical order with System imports first. 


Bad: //  
    using MovieExtravaganza2.Model; 
    using System; 
    using FilmTracker.Model; 


Good: // 
    using System; 
    using FilmTracker.Model; 
    using MovieExtravaganza2.Model; 

 
### DMC - Use camelCase for names of local variables and parameters. 

Naming: // Use camelCase with the first word lowercase and the second uppercase 


Bad: // FirstName, firstname, Firstname 


Good: // firstName 


(referencing c# at google style guide link in step 1 of instructions)  