using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlAsistencia
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }

        public DateTime Fecha1 {get; set; }
        public DateTime Fecha2 { get; set; }

        private void Reporte_Load(object sender, EventArgs e)
        {


            // TODO: esta línea de código carga datos en la tabla 'DatasetMostrarFecha.Rango_Fecha' Puede moverla o quitarla según sea necesario.
            this.Rango_FechaTableAdapter.Fill(this.DatasetMostrarFecha.Rango_Fecha, Fecha1, Fecha2);
            this.reportViewer1.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime Fecha1 = dtpDesde.Value;
            DateTime Fecha2= dtpHasta.Value;

               // TODO: esta línea de código carga datos en la tabla 'DatasetMostrarFecha.Rango_Fecha' Puede moverla o quitarla según sea necesario.
            this.Rango_FechaTableAdapter.Fill(this.DatasetMostrarFecha.Rango_Fecha, Fecha1, Fecha2);
            this.reportViewer1.RefreshReport();


        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
