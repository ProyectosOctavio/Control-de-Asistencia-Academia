using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlAsistencia
{
    public partial class Asistencia : Form
    {
        public Asistencia()
        {
            InitializeComponent();
        }

       

        private void Asistencia_Load(object sender, EventArgs e)
        {


            cmbAsistencia.Items.Add("Presente");
            cmbAsistencia.Items.Add("Ausente");
            cmbAsistencia.Items.Add("Tarde");
            cmbAsistencia.Items.Add("Justificado/a");

            cmbCinta.Items.Add("Blanca");
            cmbCinta.Items.Add("Blanca avanzada");
            cmbCinta.Items.Add("Amarilla");
            cmbCinta.Items.Add("Amarilla avanzada");
            cmbCinta.Items.Add("Verde");
            cmbCinta.Items.Add("Verde avanzada");
            cmbCinta.Items.Add("Azul");
            cmbCinta.Items.Add("Azul avanzada");
            cmbCinta.Items.Add("Roja");
            cmbCinta.Items.Add("Roja avanzada");
            cmbCinta.Items.Add("Negra");

            cmbTurno.Items.Add("Primer");
            cmbTurno.Items.Add("Segundo");
            cmbTurno.Items.Add("Tercer");




            try
              {
                using (SqlConnection conn = Conexion.Conectar())
                {
                    MessageBox.Show("Conexión exitosa");

                    dataGridView1.DataSource = llenar_grid();

                    conn.Close(); // Cerrar la conexión
                }
            }
    catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al conectar a la base de datos: " + ex.Message);
            }
        }

        public DataTable llenar_grid()
        {
            using (SqlConnection conn = Conexion.Conectar())
            {
                DataTable dt = new DataTable();
                string consulta = "SELECT * FROM DatosAsistencia";
                SqlCommand cmd = new SqlCommand(consulta, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                conn.Close(); // Cierra la conexión aquí
                return dt;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtNombres.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtApellidos.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cmbAsistencia.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtFecha.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cmbCinta.SelectedItem = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cmbTurno.SelectedItem = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtFechaNac.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                
            }

            catch
            {

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(cmbAsistencia.Text) || string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(cmbCinta.Text) || string.IsNullOrEmpty(cmbTurno.Text) || string.IsNullOrEmpty(txtFechaNac.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Está seguro que desea agregar un nuevo dato?", "Agregar nuevo dato", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    return;
                }

                using (SqlConnection conn = Conexion.Conectar())
                {
                    string insertar = "INSERT INTO DatosAsistencia (Nombre,Apellido,Asistencia,Fecha,Cinta,Turno,FechaNacimiento, Edad) VALUES (@Nombre,@Apellido,@Asistencia,@Fecha,@Cinta,@Turno,@FechaNacimiento, @Edad)";
                    using (SqlCommand cmd1 = new SqlCommand(insertar, conn))
                    {
                        cmd1.Parameters.AddWithValue("@Nombre", txtNombres.Text);
                        cmd1.Parameters.AddWithValue("@Apellido", txtApellidos.Text);
                        cmd1.Parameters.AddWithValue("@Asistencia", cmbAsistencia.SelectedItem);
                        cmd1.Parameters.AddWithValue("@Fecha", txtFecha.Text);
                        cmd1.Parameters.AddWithValue("@Cinta", cmbCinta.SelectedItem);
                        cmd1.Parameters.AddWithValue("@Turno", cmbTurno.SelectedItem);
                        cmd1.Parameters.AddWithValue("@FechaNacimiento", txtFechaNac.Text);

                        // Calcular la edad a partir de la fecha de nacimiento
                        DateTime fechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                        int edad = DateTime.Now.Year - fechaNacimiento.Year;
                        if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;

                        cmd1.Parameters.AddWithValue("@Edad", edad);

                        cmd1.ExecuteNonQuery();

                        MessageBox.Show("Los datos fueron agregados con éxito");

                        dataGridView1.DataSource = llenar_grid();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al agregar los datos: " + ex.Message);
            }
        }



        private void btnModificar_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (string.IsNullOrEmpty(txtNombres.Text) || string.IsNullOrEmpty(txtApellidos.Text) || string.IsNullOrEmpty(cmbAsistencia.Text) || string.IsNullOrEmpty(txtFecha.Text) || string.IsNullOrEmpty(cmbCinta.Text) || string.IsNullOrEmpty(cmbTurno.Text) || string.IsNullOrEmpty(txtFechaNac.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos antes de modificar los datos");
                    return;
                }

                DialogResult result = MessageBox.Show("¿Estás seguro de que deseas modificar estos datos?", "Confirmar Modificación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = Conexion.Conectar())
                    {
                        string actualizar = "UPDATE DatosAsistencia SET Nombre=@Nombre, Apellido=@Apellido, Asistencia=@Asistencia, Fecha=@Fecha, Cinta=@Cinta, Turno=@Turno, FechaNacimiento=@FechaNacimiento, Edad=@Edad WHERE Id=@Id";
                        using (SqlCommand cmd = new SqlCommand(actualizar, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", txtNombres.Text);
                            cmd.Parameters.AddWithValue("@Apellido", txtApellidos.Text);
                            cmd.Parameters.AddWithValue("@Asistencia", cmbAsistencia.SelectedItem);
                            cmd.Parameters.AddWithValue("@Fecha", txtFecha.Text);
                            cmd.Parameters.AddWithValue("@Cinta", cmbCinta.SelectedItem);
                            cmd.Parameters.AddWithValue("@Turno", cmbTurno.SelectedItem);
                            cmd.Parameters.AddWithValue("@FechaNacimiento", txtFechaNac.Text);
                            cmd.Parameters.AddWithValue("@Id", dataGridView1.CurrentRow.Cells["Id"].Value);





                            // Calcular la edad a partir de la fecha de nacimiento
                            DateTime fechaNacimiento = DateTime.Parse(txtFechaNac.Text);
                            int edad = DateTime.Now.Year - fechaNacimiento.Year;
                            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;

                            cmd.Parameters.AddWithValue("@Edad", edad);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Los datos fueron actualizados con éxito");

                            dataGridView1.DataSource = llenar_grid();
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al modificar los datos: " + ex.Message);
            }

        }


        private void btnEliminar_Click(object sender, EventArgs e)
            {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una fila para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Está seguro que desea eliminar los datos seleccionados?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = Conexion.Conectar())
                {
                    string eliminar = "DELETE FROM DatosAsistencia WHERE Id = @Id";
                    using (SqlCommand cmd3 = new SqlCommand(eliminar, conn))
                    {
                        cmd3.Parameters.AddWithValue("@Id", dataGridView1.CurrentRow.Cells["Id"].Value);

                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("Los datos fueron eliminados con éxito");

                        dataGridView1.DataSource = llenar_grid();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar los datos: " + ex.Message);
            }
        }

    

    


private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            cmbAsistencia.SelectedIndex = -1;
            txtFecha.Clear();
            cmbCinta.SelectedIndex = -1;
            cmbTurno.SelectedIndex = -1;
            txtFechaNac.Clear();
            txtBuscar.Clear();
            txtBuscarFecha.Clear();
            txtNombres.Focus();
           

        }

        private void txtNombres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtApellidos.Focus();
                e.SuppressKeyPress = true; // Para evitar que se genere un sonido al presionar Enter
            }
        }

        private void txtApellidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAsistencia.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtAsistencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFecha.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCinta.Focus();
                e.SuppressKeyPress = true;
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Conexion.Conectar())
                {
                    string consulta = "";
                    SqlCommand cmd = new SqlCommand();

                    if (string.IsNullOrEmpty(txtBuscar.Text) && string.IsNullOrEmpty(txtBuscarFecha.Text) && string.IsNullOrEmpty(txtBuscarNac.Text))
                    {
                        MessageBox.Show("Ingrese un valor para la búsqueda");
                        return;
                    }

                    else if ((!string.IsNullOrEmpty(txtBuscar.Text) ? 1 : 0) +
                    (!string.IsNullOrEmpty(txtBuscarFecha.Text) ? 1 : 0) +
                    (!string.IsNullOrEmpty(txtBuscarNac.Text) ? 1 : 0) > 1)
                    {
                        MessageBox.Show("Ingrese solo un valor para la búsqueda");
                        return;
                    }

                    else if (!string.IsNullOrEmpty(txtBuscar.Text))
                    {
                        consulta = "SELECT * FROM DatosAsistencia WHERE Nombre LIKE @Busqueda OR Apellido LIKE @Busqueda OR Asistencia LIKE @Busqueda OR Cinta LIKE @Busqueda OR Turno LIKE @Busqueda OR Edad LIKE @Busqueda";
                        cmd.Parameters.AddWithValue("@Busqueda", txtBuscar.Text);
                        
                    }
                    else if (!string.IsNullOrEmpty(txtBuscarFecha.Text))
                    {
                        consulta = "SELECT * FROM DatosAsistencia WHERE CONVERT(date, Fecha) = @BusquedaFecha";
                        cmd.Parameters.AddWithValue("@BusquedaFecha", Convert.ToDateTime(txtBuscarFecha.Text).Date);
                    }

                   else if (!string.IsNullOrEmpty(txtBuscarNac.Text))
                    {
                        consulta = "SELECT * FROM DatosAsistencia WHERE MONTH(FechaNacimiento) = MONTH(@BusquedaFechaNac) AND DAY(FechaNacimiento) = DAY(@BusquedaFechaNac)";
                        cmd.Parameters.AddWithValue("@BusquedaFechaNac", Convert.ToDateTime(txtBuscarNac.Text).Date);
                    }



                    cmd.CommandText = consulta;
                    cmd.Connection = conn;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al buscar los datos: " + ex.Message);
            }



        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbAsistencia_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtFecha.Focus();
                e.SuppressKeyPress = true;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          /*  DateTime fechaNacimiento = dtpFechaNacimiento.Value;
            DateTime fechaActual = DateTime.Today;

            int edad = fechaActual.Year - fechaNacimiento.Year;
            if (fechaActual < fechaNacimiento.AddYears(edad)) edad++;

            if (fechaNacimiento.Month == fechaActual.Month && fechaNacimiento.Day == fechaActual.Day)
            {
                MessageBox.Show("Hoy alguien cumple años. es momento de modificar las fechas de Nacimiento");
            }*/

        }

    

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Actualizamos el valor de la fecha y hora cada vez que se ejecuta el Timer
            txtFecha.Text = DateTime.Now.ToString();
         
        }

        private bool horaActualActiva = false;

        private void btnActDesact_Click(object sender, EventArgs e)
        {


            if (!horaActualActiva)
            {
                txtFecha.Text = DateTime.Now.ToString();
                // Configuramos el Timer para que se ejecute cada segundo
                timer1.Interval = 1000;
                timer1.Start();

                btnActDesact.Text = "Desactivar Hora Actual";
                horaActualActiva = true;
            }
            else
            {
                txtFecha.Text = ""; // Limpiamos el control
                timer1.Stop(); // Detenemos el Timer
                btnActDesact.Text = "Activar Hora Actual";
                horaActualActiva = false;
            }


        }

       


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbCinta_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                cmbTurno.Focus();
                e.SuppressKeyPress = true;
            }

        }

        private void cmbTurno_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtFechaNac.Focus();
                e.SuppressKeyPress = true;
            }

        }

        private void txtFechaNac_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtNombres.Focus();
                e.SuppressKeyPress = true;
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbCinta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}





