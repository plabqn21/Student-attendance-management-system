using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Student_attendance_management_system.Models.ViewModel
{
    public class FinalAttendanceViewModel
    {
       

        public List<Student> Students { get; set; }
        public List<ReceiveAttendanceListViewModel> ReceiveAttendanceListViewModels { get; set; }

       


    }
}