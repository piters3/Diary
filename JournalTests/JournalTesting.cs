using Journal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace JournalTests
{
    [TestClass]
    public class JournalTesting
    {
        TeachersJournal TJ = new TeachersJournal();

        [TestMethod]
        public void AddStudentIncreaseListLength()
        {
            int beforeAddLength = TJ.StudentsList.Count;
            TJ.AddStudent("Daniel", "Jop");
            int afterAddLength = TJ.StudentsList.Count;

            Assert.AreEqual(beforeAddLength, afterAddLength - 1);
        }


        [TestMethod]
        public void AddStudentReturnsCodeOneWhenStudentNameAndSurnameAlreadyExist()
        {
            Student st1 = TJ.StudentsList.Last();
            int code = TJ.AddStudent(st1.Name, st1.Surname);

            Assert.AreEqual(code, 1);
        }


        [TestMethod]
        public void RemoveStudentDecreaseListLength()
        {
            int beforeRemoveLength = TJ.StudentsList.Count;
            TJ.StudentsList.RemoveAt(0);
            int afterRemoveLength = TJ.StudentsList.Count;

            Assert.AreEqual(beforeRemoveLength, afterRemoveLength + 1);
        }


        [TestMethod]
        public void EditStudent()
        {
            string beforeEditName = "Daniel";
            Student st = new Student() { Name = beforeEditName };

            TJ.EditStudent(st);
            string afterEditName = st.Name;

            Assert.AreNotEqual(beforeEditName, afterEditName);
        }


        [TestMethod]
        public void AddLessonIncreaseListLength()
        {
            int beforeAddLength = TJ.Lessons.Count;
            TJ.AddLesson(DateTime.Today);
            int afterAddLength = TJ.Lessons.Count;

            Assert.AreEqual(beforeAddLength, afterAddLength - 1);
        }


        [TestMethod]
        public void AddGradeIncreaseListLength()
        {
            Presence pr = TJ.Lessons.First().PresenceList.First();
            int beforeAddLength = pr.Grades.Count;
            TJ.AddGrade(pr);
            int afterAddLength = pr.Grades.Count;

            Assert.AreEqual(beforeAddLength, afterAddLength - 1);
        }


        [TestMethod]
        public void ComputeAverageForStudent()
        {
            var pr = new Presence(TJ.StudentsList[0], true) { Grades = new ObservableCollection<int> { 3, 5, 4 } };

            double computedAverage = TJ.ComputeStudentAverage(pr);

            Assert.AreEqual(computedAverage, 4.0);
        }


        [TestMethod]
        public void ComputeAverageForGoup()
        {
            var lesson = TJ.Lessons[1].PresenceList;
            double average = TJ.ComputeGroupAverage(lesson);

            Assert.AreEqual(average, 2.0);
        }


        [TestMethod]
        public void SaveToFileCreatesFileIfNoExist()
        {
            string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LessonsList.txt";

            bool isFileExist = File.Exists(file);

            if (!isFileExist)
            {
                TJ.SaveToFile();
            }

            Assert.AreNotEqual(isFileExist, File.Exists(file));
        }


        [TestMethod]
        public void SaveToFileWritesDataToFile()
        {
            string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\LessonsList.txt";

            TJ.SaveToFile();

            int fileLength = file.Length;

            Assert.IsTrue(fileLength > 0);
        }


        [TestMethod]
        public void ReadFromFileFillLessonsList()
        {
            TJ.Lessons.Clear();
            int beforeReadFromFileListLength = TJ.Lessons.Count;

            TJ.ReadFromFile();
            int afterReadFromFileListLength = TJ.Lessons.Count;

            Assert.AreNotEqual(beforeReadFromFileListLength, afterReadFromFileListLength);
        }


        [TestMethod]
        public void ReadFromFileFilledStudentsListIsNotNull()
        {
            TJ.StudentsList.Clear();
            TJ.ReadFromFile();
            int afterReadFromFileListLength = TJ.StudentsList.Count;

            Assert.IsTrue(afterReadFromFileListLength > 0);
        }


        [TestMethod]
        public void IsAbsentIncreaseStudentAbsenceNumber()
        {
            int absenceNumberBeforeAdd = 0;
            Student st = new Student() { AbsencesNumber = absenceNumberBeforeAdd };

            TJ.IsAbsent(st);
            int absenceNumberAfterAdd = st.AbsencesNumber;

            Assert.AreEqual(absenceNumberBeforeAdd, absenceNumberAfterAdd - 1);
        }


        [TestMethod]
        public void IsPresentDecreaseStudentAbsenceNumber()
        {
            int absenceNumberBeforeRemove = 1;
            Student st = new Student() { AbsencesNumber = absenceNumberBeforeRemove };

            TJ.IsPresent(st);
            int absenceNumberAfterRemove = st.AbsencesNumber;

            Assert.AreEqual(absenceNumberBeforeRemove, absenceNumberAfterRemove + 1);
        }
    }
}
