using DocumentFormat.OpenXml.Office.CustomUI;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_spare_parts
{
	public partial class Form_control : Form
	{

		public Form_control()
		{
			InitializeComponent();
			Form1.EventoClientesLlegan += ActualizarClientesIn;
			Form1.EventoClientesParten += ActualizarClientesOut;
			Form1.EventoPizza += ActualizarPizzas;
			Form1.EventoPapas += ActualizarPapas;
			Form1.EventoBoneless += ActualizarBoneless;
			Form1.EventoCombo += ActualizarCombs;
			Form1.EventoFila += ActualizarFila;
			Form1.EventoMesas += ActualizarMesas;

			Form1.ComenzarReloj += AdministrarReloj;
			Reloj.EventoHoraCambio += CambiarHora;
			Reloj.EventoGenerarDocumento += GenerarDocumento;
		}



		public delegate void TiempoLlegadaDelegate(int mil);
		public delegate void TiempoOrdenDelegate(int mil);
		public delegate void TiempoComerDelegate(int i, int mil);
		public delegate void EmpleadosEnCajaDelegate(int cant);
		public delegate void TiempoPreparacionDelegate(int i, int mil);
		public delegate void VelocidadTiempoDelegate(int valor);
		public delegate void CantidadMesasDelegate(int i);
		public event TiempoLlegadaDelegate TiempoLlegadaEvent;
		public event TiempoOrdenDelegate TiempoOrdenEvent;
		public event TiempoComerDelegate TiempoComerEvent;
		public event EmpleadosEnCajaDelegate EmpleadosEnCajaEvent;
		public event TiempoPreparacionDelegate TiempoPreparacionEvent;
		public event VelocidadTiempoDelegate VelocidadTiempoEvent;
		public event CantidadMesasDelegate CantidadMesasEvent;

		public Action closeForm;

		Reloj reloj_simulado;
		private void AdministrarReloj()
		{
			reloj_simulado = new Reloj(tb_velocidad.Value);
			reloj_simulado.cerrarForma = closeForm;
			reloj_simulado.setError = setError;
		}
		private void CambiarHora(string hora)
		{
			lbl_hora.Invoke(new Action(() => lbl_hora.Text = hora));
		}

		private void btn_update_Click(object sender, EventArgs e)
		{
			int velocidad = tb_velocidad.Value;
			EmpleadosEnCajaEvent(Convert.ToInt32(txt_en_caja.Text) * 1000);
			CantidadMesasEvent(Convert.ToInt32(txt_mesas.Text));
			TiempoPreparacionEvent(0, (Convert.ToInt32(txt_pizza_time.Text) * 1000) / velocidad);
			TiempoPreparacionEvent(1, (Convert.ToInt32(txt_boneless_time.Text) * 1000) / velocidad);
			TiempoPreparacionEvent(2, (Convert.ToInt32(txt_papas_time.Text) * 1000) / velocidad);
			TiempoPreparacionEvent(3, (Convert.ToInt32(txt_papas_time.Text) * 1000) / velocidad);

			TiempoLlegadaEvent(Convert.ToInt32(txt_tiempo_llegada.Text) * 1000);
			TiempoComerEvent(0, Convert.ToInt32(txt_tiempo_comer_pizza.Text) * 1000 / velocidad);
			TiempoComerEvent(1, Convert.ToInt32(txt_tiempo_comer_boneless.Text) * 1000 / velocidad);
			TiempoComerEvent(2, Convert.ToInt32(txt_tiempo_comer_papa.Text) * 1000 / velocidad);
			TiempoComerEvent(3, Convert.ToInt32(txt_tiempo_comer_combo.Text) * 1000 / velocidad);

			VelocidadTiempoEvent(tb_velocidad.Value);
		}
		private void ActualizarClientesIn(string cant)
		{
			lbl_clientes_in.Text = cant;
		}
		private void ActualizarClientesOut(string cant)
		{
			lbl_clientes_out.BeginInvoke(new Action(() => lbl_clientes_out.Text = cant));
		}
		private void ActualizarPizzas(string cant)
		{
			lbl_pizza_out.BeginInvoke(new Action(() => lbl_pizza_out.Text = cant));
		}
		private void ActualizarBoneless(string cant)
		{
			lbl_boneless_out.BeginInvoke(new Action(() => lbl_boneless_out.Text = cant));
		}
		private void ActualizarPapas(string cant)
		{
			lbl_papas_out.BeginInvoke(new Action(() => lbl_papas_out.Text = cant));
		}
		private void ActualizarCombs(string cant)
		{
			lbl_combo_out.BeginInvoke(new Action(() => lbl_combo_out.Text = cant));
		}
		private void ActualizarMesas(string i)
		{
			lbl_en_mesas.BeginInvoke(new Action(() => lbl_en_mesas.Text = i));
		}

		private void ActualizarFila(string i)
		{
			lbl_en_fila.BeginInvoke(new Action(() => lbl_en_fila.Text = i));
		}

		void setError()
		{
			Invoke(new Action(() =>
			{
				errorProvider1.SetError(txt_num_reporte, "Ha terminado el turno");
			}));
		}

		public void GenerarDocumento()
		{
			int estado;

			using (StreamReader sr = new StreamReader("estado.txt"))
			{
				estado = int.Parse(sr.ReadLine());
				if (estado == 1)
				{
					sr.Close();
					using (StreamWriter wt = new StreamWriter("estado.txt"))
					{
						wt.WriteLine("2");
					}
				}
			}

			SLDocument reporte = new SLDocument();

			int[] variables = {
				int.Parse(txt_en_caja.Text),
				int.Parse(txt_mesas.Text),
				int.Parse(txt_pizza_time.Text),
				int.Parse(txt_boneless_time.Text),
				int.Parse(txt_papas_time.Text),
				int.Parse(txt_combo_time.Text),
				int.Parse(txt_tiempo_llegada.Text),
				int.Parse(txt_tiempo_comer_pizza.Text),
				int.Parse(txt_tiempo_comer_boneless.Text),
				int.Parse(txt_tiempo_comer_papa.Text),
				int.Parse(txt_tiempo_comer_combo.Text),
				int.Parse(lbl_clientes_in.Text),
				int.Parse(lbl_en_fila.Text),
				int.Parse(lbl_en_mesas.Text),
				int.Parse(lbl_clientes_out.Text),
				int.Parse(lbl_pizza_out.Text),
				int.Parse(lbl_boneless_out.Text),
				int.Parse(lbl_papas_out.Text),
				int.Parse(lbl_combo_out.Text)
			};

			string[] palabras = {
				"En caja",
				"Mesas",
				"Tiempo de preparacion de pizza",
				"Tiempo de preparacion de boneless",
				"Tiempo de preparacion de papas",
				"Tiempo de preparacion de combos",
				"Tiempo de llegada",
				"Tiempo de comer pizza",
				"Tiempo de comer boneless",
				"Tiempo de comer Papas",
				"Tiempo de comer Combo",
				"Entraron",
				"En fila",
				"En mesas",
				"Salieron",
				"Pizzas compradas",
				"Boneless comprados",
				"Papas compradas",
				"Combos comprados"
			};

			if (estado == 2)
			{
				using (StreamWriter wt = new StreamWriter("estado.txt"))
				{
					wt.WriteLine("1");
				}

				reporte = new SLDocument(string.Format($"Reporte{int.Parse(txt_num_reporte.Text)}.xlsx"));
				for (int i = 0; i < variables.Length; i++)
				{
					reporte.SetCellValue(i + 1, 3, variables[i]);
				}
				reporte.Save();
				Process.Start(string.Format($"Reporte{int.Parse(txt_num_reporte.Text)}.xlsx"));
			}
			else
			{
				for (int i = 0; i < palabras.Length; i++)
				{
					reporte.SetCellValue(i + 1, 1, palabras[i]);
					reporte.SetCellValue(i + 1, 2, variables[i]);
				}
				 reporte.SaveAs(string.Format($"Reporte{int.Parse(txt_num_reporte.Text)}.xlsx"));
			}
		}

	}
	class Reloj
	{
		public delegate void DelegadoHora(string hora);
		public static event DelegadoHora EventoHoraCambio;
		DateTime hora = new DateTime(2000, 1, 1, 10, 0, 0, 0);

		public delegate void DelegadoStop();
		public static event DelegadoStop EventoGenerarDocumento;


		public Action cerrarForma;
		public Action setError;

		public Reloj(int velocidad)
		{
			Task.Run(() =>
			{
				while (true)
				{
					hora = hora.AddMinutes(1);
					Thread.Sleep(1000 / velocidad);
					EventoHoraCambio(ObtenerHoraActual());
					if (hora.Hour == 22 && hora.Minute == 30)
					{
						SoundPlayer sp = new SoundPlayer("FNAF - 6 AM sound.wav");
						sp.Play();
						setError();
						Thread.Sleep(10000);
						//aqui guardamos los resultados
						EventoGenerarDocumento?.Invoke();
						cerrarForma();
						Application.Exit();
						break;
					}
				}
			});
		}
		public string ObtenerHoraActual()
		{
			return hora.ToString("HH:mm:ss");
		}
	}
}
