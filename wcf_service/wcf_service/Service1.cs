using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_service
{
    // 참고: "리팩터링" 메뉴에서 "이름 바꾸기" 명령을 사용하여 코드 및 config 파일에서 클래스 이름 "Service1"을 변경할 수 있습니다.
    public class Service1 : IService1
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=student;Uid=root;Pwd=wjsrhkdgh.1;");
        MySqlDataAdapter mySqlDataAdapter;
        DataSet DB = new DataSet();

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public DataSet readDB()
        {
            try {
                mySqlDataAdapter = new MySqlDataAdapter("SELECT * FROM student;", connection);
                mySqlDataAdapter.Fill(DB, "Databases");
            }
            catch { }
            return DB;
        }

        public DataSet createDB(String[] st)
        {
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("insert into student values(" + st[0] + "," + st[1] + "," + st[2] + ",'" + st[3] + "','" + st[4] + "');", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
            return DB;
        }

        public DataSet updateDB(DataTable dt)
        {
            try { 
                connection.Open();
                DataTable dtChanged = dt;
                foreach (DataRow row in dtChanged.Rows)
                {
                    if (row.RowState == DataRowState.Modified)
                    {
                        object beforevalue = null;
                        object aftervalue = null;
                        foreach (DataColumn col in dtChanged.Columns)
                        {
                            beforevalue = row[col, DataRowVersion.Original];
                            aftervalue = row[col, DataRowVersion.Current];

                            MySqlCommand command = new MySqlCommand("update student set " + col.ToString() + "= '" + aftervalue.ToString() + "'where no=" + row[2].ToString(), connection);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
            catch { }
            return DB;
        }

        public DataSet removeDB(String no)
        {
            try {
                connection.Open();
                MySqlCommand command = new MySqlCommand("DELETE  FROM student WHERE no=" + no, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch { }
            return DB;
        }
    }
}
