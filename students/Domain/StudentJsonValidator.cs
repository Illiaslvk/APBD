using Newtonsoft.Json.Schema;

namespace students
{
    public class StudentJsonValidator
    {
        private static readonly string JsonSchema = @"{
          'description': 'A student',
          'type': 'object',
          'properties': {
            'firstName': {'type': 'string'},
            'lastName': {'type': 'string'},
            'index': {'type': 'string', 'pattern': 's[0-9]+'},
            'birthDate': {'type': 'string'},
            'studies': {'type': 'string'},
            'mode': {'type': 'string'},
            'email': {'type': 'string'},
            'fatherName': {'type': 'string'},
            'motherName': {'type': 'string'}
          },
          'required': ['firstName', 'lastName', 'index', 'birthDate', 'studies', 'mode', 'email', 'fatherName', 'motherName']
        }";

        private readonly JSchema schema = JSchema.Parse(JsonSchema);

        public bool IsValid(object studentJson)
        {
            if (studentJson == null)
            {
                return false;
            }
            Console.WriteLine(studentJson.ToString());
            Newtonsoft.Json.Linq.JObject studentObj
                = Newtonsoft.Json.Linq.JObject.Parse(studentJson.ToString());
            Console.WriteLine(studentObj.IsValid(schema));
            return studentObj.IsValid(schema);
        }
    }
}
