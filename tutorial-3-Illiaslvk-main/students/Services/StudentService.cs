using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace students
{
    public class StudentService
    {
        private const string PATH_TO_DATABASE = "students.csv";
        private readonly StudentJsonValidator validator = new();

        public List<Student> GetAll()
        {
            var students = File.ReadAllLines(PATH_TO_DATABASE);
            return students.Where(student => !student.Equals(""))
                        .Select(student => ParseLineIntoStudent(student))
                        .ToList<Student>();
        }

        public Student GetByIndex(string index)
        {
            using var reader = new StreamReader(PATH_TO_DATABASE);
            Student? student = null;
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(index))
                {
                    student = ParseLineIntoStudent(line);
                    break;
                }
            }
            reader.Close();
            return student;
        }

        public Student Add(object postBody)
        {
            if (!validator.IsValid(postBody))
            {
                return null;
            }
            Student? student = JsonConvert.DeserializeObject<Student>(postBody.ToString());

            using var reader = new StreamReader(PATH_TO_DATABASE);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(student.Index))
                {
                    reader.Close();
                    return null;
                }
            }
            reader.Close();
            File.AppendAllLines(PATH_TO_DATABASE, new List<string>() { student.ToCsvString() });
            return student;
        }

        public Student Modify(object studentObj)
        {
            if (!validator.IsValid(studentObj))
            {
                return null;
            }
            Student? student = JsonConvert.DeserializeObject<Student>(studentObj.ToString());

            string? line = File.ReadAllLines(PATH_TO_DATABASE)
                .Where(line => line.Contains(student.Index))
                .First();

            if (line == null)
            {
                return null;
            }

            string index = getIndexFromCsv(line);
            Delete(index);
            return Add(studentObj);
        }

        private static string getIndexFromCsv(string studentCsv)
        {
            Regex rx = new Regex(@"s[0-9]+");
            MatchCollection matches = rx.Matches(studentCsv);
            return matches.First().Value;
        }

        public static DateTime GetDateFromString(string date)
        {
            try
            {
                var dateParts = date.Split("/");
                var month = dateParts[0].Length == 1 ? "0" + dateParts[0] : dateParts[0];
                var day = dateParts[1].Length == 1 ? "0" + dateParts[1] : dateParts[1];
                var year = dateParts[2];

                var properFormat = month + "/" + day + "/" + year;

                return DateTime.ParseExact(properFormat, "MM/dd/yyyy", null);
            } catch (IndexOutOfRangeException e)
            {
                throw new ArgumentException(date, nameof(date), e);
            }

        }

        public static string ConvertDateToString(DateTime date)
        {
            return date.Month + "/" + date.Day + "/" + date.Year;
        }

        public int Delete(string index)
        {
            try
            {
                var lines = File.ReadAllLines(PATH_TO_DATABASE).Where(line => !line.Contains(index));
                File.WriteAllLines(PATH_TO_DATABASE, lines);
                return 0;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        private static Student ParseLineIntoStudent(string line)
        {
            var args = line.Split(",");

            return new Student
            (
                args[0],
                args[1],
                args[2],
                args[3],
                args[4],
                args[5],
                args[6],
                args[7],
                args[8]
            );
        }

    }
}
