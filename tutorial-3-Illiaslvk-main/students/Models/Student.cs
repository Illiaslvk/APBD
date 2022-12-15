using Newtonsoft.Json;

namespace students
{
    public class Student
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get{ return _lastName; }
            set{ _lastName = value; }
        }

        private string _index;
        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private DateTime _birthDate;

        public string BirthDate
        {
            get { return _birthDate.ToString("MM/dd/yyyy"); }
            set { _birthDate = StudentService.GetDateFromString(value); }
        }

        private string _studies;
        public string Studies
        {
            get { return _studies; }
            set { _studies = value; }
        }

        private string _mode;
        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _fatherName;
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }

        private string _motherName;
        public string MotherName
        {
            get { return _motherName; } 
            set { _motherName = value;  }
        }

        [JsonConstructor]
        public Student(string firstName,
            string lastName,
            string index,
            string birthDate,
            string studies,
            string mode,
            string email,
            string fatherName,
            string motherName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
            this._index = index;
            this._birthDate = StudentService.GetDateFromString(birthDate);
            this._studies = studies;
            this._mode = mode;
            this._email = email;
            this._fatherName = fatherName;
            this._motherName = motherName;
        }

        public string ToCsvString()
        {
            return this._firstName + "," + this._lastName + "," + 
                this._index + "," + StudentService.ConvertDateToString(this._birthDate) + "," +
                this._studies + "," + this._mode + "," + this._email + "," +
                this._fatherName + "," + this._motherName;
        }

        public void CheckIfNullFields()
        {
            if (this._firstName == null ||
            this._lastName == null ||
            this._index == null ||
            this._studies == null ||
            this._mode == null ||
            this._email == null ||
            this._fatherName == null ||
            this._motherName == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
