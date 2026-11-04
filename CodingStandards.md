# C# Coding Standards

### 1. Space Indentation (DMC)

**Indentation:** // Use 4 spaces for indentation when writing code.

**Bad:** //
```csharp
public App() 
{ 
  InitializeComponent(); 

      // The app is designed dark 
      UserAppTheme = AppTheme.Dark; 
} 
```

**Good:** //
```csharp
public App() 
{ 
    InitializeComponent(); 

    // The app is designed dark 
    UserAppTheme = AppTheme.Dark; 
} 
```

### 2. Use PascalCase for naming classes, methods, enumerations, public fields, and public properties. (DMC)

**Naming:** // Capitalize every word.

**Bad:**
```csharp
homePage
homepage
Homepage
```

**Good:**
```csharp
HomePage
```

### 3. Namespace ‘using’ declarations go at the top of the file before any namespaces. Organize the ‘using’ imports by alphabetical order and have System imports always organized first. t (DMC)

**Organize:** // declarations at the top in alphabetical order with System imports first.

**Bad:**
```csharp
using MovieExtravaganza2.Model; 
using System; 
using FilmTracker.Model; 
```

**Good:**
```csharp
using System; 
using FilmTracker.Model; 
using MovieExtravaganza2.Model; 
```

### 4.  Use camelCase for names of local variables and parameters. (DMC)

**Naming:** // Use camelCase with the first word lowercase and the second uppercase.

**Bad:**
```csharp
FirstName
firstname
Firstname
```

**Good:**
```csharp
firstName
```

(referencing C# at Google Style Guide)

### 5. Filenames and directory names are PascalCase (BC)

**Naming:** // Use PascalCase for filenames and directory names

**Bad:**
```csharp
my_file.cs
mydirectory
```

**Good:**
```csharp
MyFile.cs
MyDirectory
```

### 6. Where possible the file name should be the same as the name of the main class in the file (BC)

**Naming:** // The class name should be the same as the file name

**Bad:**
```csharp
// Utility.cs
class OrderProcessor { }
```

**Good:**
```csharp
// OrderProcessor.cs
class OrderProcessor { }
```

### 7. Names of private, protected, internal and protected internal fields and properties (BC)

**Naming:** // Names of private, protected, internal, and protected internal fields and properties use _camelCase.

**Bad:**
```csharp
private int count;
private string Name;
```

**Good:**
```csharp
private int _count;
private string _name;
```

### 8. In general, prefer one core class per file. (BC)

**Organization:** // In general, prefer one core class per file.

**Bad:**
```csharp
// ShopSystem.cs
class Customer { }
class Order { }
class Product { }
```

**Good:**
```csharp
// Customer.cs
class Customer { }

// Order.cs
class Order { }

// Product.cs
class Product { }
```

### 9. Modifiers must be declared in the exact specified order (LJ)

**Ordering:** // order: public protected internal private new abstract virtual override sealed static readonly extern unsafe volatile async.

**Bad:**
```csharp
static public int Counter = 0;
```

**Good:**
```csharp
public static int Counter = 0;
```

### 10. Class Member Grouping and Ordering (LJ)

**Grouping:** // Group and order class members by type and access level. 

**Bad:**
```csharp
private void Helper() { }
public int Count { get; set; }
public MyClass() { }
```

**Good:**
```csharp
public int Count { get; set; }
public MyClass() { }
private void Helper() { }
```

### 11. Variable var Usage Guidelines (LJ)

**Usage:** // Use var to add readability rather than mask core types. 

**Bad:**
```csharp
var success = true;
var number = 12 * ReturnsFloat();
```

**Good:**
```csharp
var apple = new Apple();
bool success = true;
```

### 12. Interface Member Implementation (LJ)

**Grouping:** // Group interface implementations together within classes.

**Bad:**
```csharp
public class Service : IMyInterface
{
    public void UnrelatedMethod() { }
    public void InterfaceMethodA() { }
    public void AnotherUnrelatedMethod() { }
    public void InterfaceMethodB() { }
}
```

**Good:**
```csharp
public class Service : IMyInterface
{
    public void InterfaceMethodA() { }
    public void InterfaceMethodB() { }

    public void UnrelatedMethod() { }
    public void AnotherUnrelatedMethod() { }
}
```

# SQL Coding Standards

### 1. SQL naming convention should be lowercase and snake_case (AB)

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

### 2. SQL keywords use uppercase (AB)

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

### 3. SQL spacing use consistent spacing (AB)

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

### 4. SQL indentation use four spaces (AB)

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