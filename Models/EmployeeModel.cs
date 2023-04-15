namespace RareHomeTest.Models
{
    // Class that maps onto the retrieved JSON structure
    public class EmployeeModel
    {
        public string Id { get; set; }
        public string? EmployeeName { get; set; }
        // The start time in the JSON data is spelled as "StarTimeUtc" so I'm sticking with it
        public DateTime StarTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string? EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    // Class that represents the employee object needed for the View
    public class Employee
    {
        public string? Name { get; set; }
        public double TotalTimeWorked { get; set; }
        // Return a human friendly string with hours and minutes
        public string GetHoursAndMinutes()
        {
            int hours = (int)Math.Floor(TotalTimeWorked);

            // Converting double to fraction
            double fraction = TotalTimeWorked - hours;

            // Multiply the fraction by 60 to get the minutes
            int minutes = (int)Math.Round(fraction * 60);         
            return $"{hours}h {minutes}m";
        }
    }

}
