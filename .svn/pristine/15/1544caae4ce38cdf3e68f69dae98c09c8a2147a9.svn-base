<%@ WebHandler Language="C#" Class="usuarios" %>

using System;
using System.Web;
using System.Text;
using System.Web.SessionState;
public class usuarios : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        int echo = Int32.Parse(context.Request["draw"]);
        int displayLength = Int32.Parse(context.Request["length"]);
        int displayStart = Int32.Parse(context.Request["start"]);
        string[] columnas = new string[] { "nombreCompleto", "tipoUsuario", "Permisos","TipoSoporte" };
        string search = context.Request["search[value]"];
        ///////////
        //SEARCH (filter)
        //- build the where clause
        ////////
        StringBuilder where = new StringBuilder();
        string whereClause = string.Empty;

        if (!String.IsNullOrEmpty(search))
        {
            where.Append(" WHERE");
            foreach (string col in columnas)
            {
                where.Append("  " + col + " LIKE '%" + search + "%' OR");
            }
            whereClause = where.ToString().Substring(0, (where.ToString().Length - 2));
        }

        ///////////////
        //ORDERING
        //- build the order by clause
        //////////////
        StringBuilder orderBy = new StringBuilder();
        string orderByClause = string.Empty;
        //Check which column is to be sorted by in which direction
        for (int i = 0; i < columnas.Length; i++)
        {
            if (context.Request.Params["columns[" + i + "][orderable]"] == "true")
            {
                orderBy.Append(context.Request.Params["order" + i + "][column]"]);
                orderBy.Append(" ");
                orderBy.Append(context.Request.Params["order[" + i + "][dir]"]);
            }
        }
        orderByClause = orderBy.ToString();
        //Replace the number corresponding the column position by the corresponding name of the column in the database
        if (!String.IsNullOrEmpty(orderByClause))
        {
            for (int i = 0; i < columnas.Length; i++)
            {
                orderByClause = orderByClause.Replace(i.ToString(), ", " + columnas[i] + "");
            }

            //Eliminate the first comma of the variable "order"
            orderByClause = orderByClause.Remove(0, 1);
        }
        else
        {
            orderByClause = "" + columnas[0] + " ASC";
        }
        orderByClause = "ORDER BY nombreCompleto " + orderByClause;

        /////////////
        //T-SQL query
        //- ROW_NUMBER() is used for db side pagination
        /////////////
        string query = " SELECT * FROM ( SELECT ROW_NUMBER() OVER ({0}) AS RowNumber,* FROM ( SELECT ( SELECT COUNT(*) " +
                        " FROM vUsuariosERPM {1} ) AS TotalDisplayRows, " +
                        " (SELECT COUNT(*) FROM vUsuariosPermisos) AS TotalRows, " +
                        " nombreCompleto, tipoUsuario,permisos,TipoSoporte  " +
                        " from  vUsuariosPermisos {1} ) RawResults) Results " +
                        " WHERE RowNumber BETWEEN {2} AND {3}";

        query = String.Format(query, orderByClause, whereClause, displayStart + 1, displayStart + displayLength);

        //Get result rows from DB
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBSGICEConnectionString"].ConnectionString);
        conn.Open();
        System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = query;
        cmd.CommandType = System.Data.CommandType.Text;
        System.Data.IDataReader rdrBrowsers = cmd.ExecuteReader();
        
        StringBuilder json = new StringBuilder();
        string outputJson = string.Empty;
        int totalDisplayRecords = 0;
        int totalRecords = 0;
        while (rdrBrowsers.Read())
        {
            if (totalRecords == 0)
                totalRecords = Int32.Parse(rdrBrowsers["TotalRows"].ToString());
            if (totalDisplayRecords == 0)
                totalDisplayRecords = Int32.Parse(rdrBrowsers["TotalDisplayRows"].ToString());
            json.Append("[");
            for (int i = 0; i < columnas.Length; i++)
            {
                if ((i + 1) == columnas.Length)
                {
                    json.Append("\"" + rdrBrowsers[columnas[i]] + "\"");
                }
                else
                {
                    json.Append("\"" + rdrBrowsers[columnas[i]] + "\",");
                }
            }
            json.Append("],");
        }
        outputJson = json.ToString();
        if (!outputJson.Equals(""))
        {
            outputJson = outputJson.Remove(outputJson.Length - 1);
        }

        StringBuilder response = new StringBuilder();

        response.Append("{");
        response.Append("\"draw\": ");//  sEcho
        response.Append(echo);
        response.Append(",");
        response.Append("\"iTotalRecords\": ");
        response.Append(totalRecords);
        response.Append(",");
        response.Append("\"iTotalDisplayRecords\": ");
        response.Append(totalDisplayRecords);
        response.Append(",");
        response.Append("\"aaData\": [");
        response.Append(outputJson);
        response.Append("]}");
        outputJson = response.ToString();

        /////////////
        /// Write to Response
        /// - clear other HTML elements
        /// - flush out JSON output
        /// ///////////
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.Write(outputJson);
        context.Response.Flush();
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}