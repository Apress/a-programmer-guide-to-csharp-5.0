using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter_07_Other_Class_Details
{
// EmployeeForm.Designer.cs
partial class EmployeeForm
{
    public void Initialize()
    {
        StartInitialization();
        FormSpecificInitialization();
        FinishInitialization();
    }

    void StartInitialization() { }
    void FinishInitialization() { }
}

// EmployeeForm.cs
partial class EmployeeForm
{
    void FormSpecificInitialization()
    {
        // add form-specific initialization code here. 
    }
}

// SalaryForm.Designer.cs
partial class SalaryForm
{
    public void Initialize()
    {
        StartInitialization();
        FormSpecificInitialization();
        FinishInitialization();
    }

    void StartInitialization() { }
    partial void FormSpecificInitialization();
    void FinishInitialization() { }
}

// SalaryForm.cs
partial class SalaryForm
{
    partial void FormSpecificInitialization()
    {
        // add form-specific initialization code here. 
    }
}
}
