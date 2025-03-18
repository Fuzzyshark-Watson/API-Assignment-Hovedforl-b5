using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Models
{
    //0: ServersideProgrammering 2
    //1: Tværfagligt Projekt
    //2: ProjektStyring
    //3: ITSM 2
    //4: AppProgrammering 2
    //5: Software Arkitektur
    public class Subject
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        private string? SubjectName { get; set; }
        private string? SubjectLongName { get; set; }
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
        public ICollection<SubjectTeacher> SubjectTeachers { get; set; } = new List<SubjectTeacher>();
        public ICollection<EducationClassSubject> EducationClassSubjects { get; set; } = new List<EducationClassSubject>();


        public Subject() {
            Refresh();
        }

        public void Refresh()
        {
            switch (Id)
            {
                case 0:
                    SubjectName = "ServersideProgrammering 2";
                    SubjectLongName = "16477_AV_V10.XX_SERVERSIDEPROGRAMMERING_2_H5PD011125";
                    break;
                case 1:
                    SubjectName = "Tværfagligt Projekt";
                    SubjectLongName = "00000_10.1_TVAÆRFAGLIGT_PROJEKT_H5PD011125";
                    break;
                case 2:
                    SubjectName = "ProjektStyring";
                    SubjectLongName = "06277_AV_10.1_PROJEKTSTYRING_H5PD011125";
                    break;
                case 3:
                    SubjectName = "ITSM 2";
                    SubjectLongName = "06256_RU_V10.x_ITSM_2_H5PD011125";
                    break;
                case 4:
                    SubjectName = "AppProgrammering 2";
                    SubjectLongName = "16479_AV_V09.X_APP_PROGRAMMERING_II_2H5PD011125";
                    break;
                case 5:
                    SubjectName = "Software Arkitektur";
                    SubjectLongName = "00000_AV_V10.1_Software_Arkitektur_H5PD011125";
                    break;
            }

        }
    }
}
