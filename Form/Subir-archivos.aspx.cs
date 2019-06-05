using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calificaciones.Form
{
    public partial class Subir_archivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(upData.FileName);
           
           upData.SaveAs(Server.MapPath("../Files/") + filename);
            string route =Server.MapPath("../Files/") + filename;
            // lblStatus .Text = "Archivo subido con éxito, ahora puede consultar su información.";
            // Ruta = Server.MapPath("Archivos/") + filename;
            // Extension = Path.GetExtension(upData.PostedFile.FileName);
            //btnGenerarInformacion.Visible = true;

            ShowData(route); 
        }


        private void ShowData(string routeguide )
        {


            string conStr = "";
            conStr=ConfigurationManager.ConnectionStrings["Excel07ConString"]
                          .ConnectionString;


          
            conStr = String.Format(conStr, routeguide, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dtInformation = new DataTable();
            cmdExcel.Connection = connExcel;

            
            
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();


            cmdExcel.CommandText = "SELECT * FROM [" + SheetName + "] ";



            oda.SelectCommand = cmdExcel;
                oda.Fill(dtInformation);
                connExcel.Close();

            table.AutoGenerateColumns = false;
                table.DataSource = dtInformation;

            table.DataBind();
            }
        }
    }
