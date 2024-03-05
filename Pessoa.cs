using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using System.Security.Policy;

namespace DesktopMySQL
{
    public class Pessoa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Cidade { get; set;}

        MySqlConnection con = new MySqlConnection("server=sql.freedb.tech;port=3306;database=freedb_MucaFreeDB_TDS10;user id=freedb_MucaLopes;password=kwsdhj5S?pnKb92;charset=utf8");

        public List<Pessoa> listapessoa()
        {
            List<Pessoa> li = new List<Pessoa>();
            string sql = "SELECT * FROM pessoa";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pessoa p = new Pessoa();
                p.ID = (int)dr["ID"];
                p.Nome = dr["Nome"].ToString();
                p.Idade = dr["Idade"].ToString();
                p.Cidade = dr["Cidade"].ToString();
                li.Add(p);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string Nome, string Idade, string Cidade)
        {
            string sql = "INSERT INTO pessoa(Nome,Idade,Cidade) VALUES ('" + Nome + "','" + Idade + "','" + Cidade + "')";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Atualizar(Pessoa pessoa)
        {
            string sql = "UPDATE pessoa SET Nome= '" + pessoa.Nome + "' , Idade= ' " + pessoa.Idade + " ' , Cidade= '" + pessoa.Cidade + "' WHERE ID= '" + pessoa.ID + "' ";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Excluir(Pessoa pessoa)
        {
            string sql = "DELETE FROM pessoa WHERE ID='" + pessoa.ID + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Localizar(Pessoa pessoa)
        {
            string sql = "SELECT * FROM pessoa WHERE ID='" + pessoa.ID + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Nome = dr["Nome"].ToString();
                Idade = dr["Idade"].ToString();
                Cidade = dr["Cidade"].ToString();
            }
            dr.Close();
            con.Close();
        }

        public bool RegistroRepetido(string Nome, string Idade, string Cidade)
        {
            string sql = "SELECT * FROM pessoa WHERE Nome= '" + Nome + "' AND Idade= ' " + Idade + " ' AND Cidade= '" + Cidade + "'";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}
