using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proyecto_spare_parts
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		Form_control frm = new Form_control();
		private void Form1_Load(object sender, EventArgs e)
		{
			EventoLLegadaMesas += EscogerMesa;
			Caja.EventoClienteAtendido += AvanzarFilaCaja;
			frm.Show();
			frm.closeForm = CerrarForma;
			frm.TiempoLlegadaEvent += EstablecerTiempoLlegada;
			frm.TiempoOrdenEvent += EstablecerTiempoOrden;
			frm.TiempoComerEvent += EstablecerTiempoComer;
			frm.EmpleadosEnCajaEvent += EstablecerEmpleadoseEnCaja;
			frm.TiempoPreparacionEvent += EstablecerTiempoPreparacion;
			frm.VelocidadTiempoEvent += EstablecerVelodicadTiempo;
			frm.CantidadMesasEvent += EstablecerCantidadMesas;
		}

		public void CerrarForma()
		{
			Invoke(new Action(() =>
			{
				Hide();
			}));
		}

		List<Cliente> fila_caja = new List<Cliente>();
		List<Cliente> clientes_en_mesa = new List<Cliente>();
		List<Mesa> mesas = new List<Mesa>();
		private void AvanzarFilaCaja()
		{
			////If a client while and another spawns and comes to the queue, this stands a place behind and the next client steps over them
			//for (int i = 0; i < fila_caja.Count-1; i++)
			//{
			//    fila_caja[i] = fila_caja[i + 1];
			//}
			//clientes_en_mesa.Add(fila_caja[fila_caja.Count()-1]);
			////if(fila_caja.Count>0)
			////    fila_caja.RemoveAt(fila_caja.Count - 1);
			//foreach (var cliente in fila_caja)
			//{
			//    Point next_step = new Point(cliente.cliente_holder.Location.X, cliente.cliente_holder.Location.Y- cliente.cliente_holder.Size.Height);
			//    cliente.cliente_holder.Location = next_step;
			//}
			for (int i = 0; i < fila_caja.Count - 1; i++)
			{
				fila_caja[i] = fila_caja[i + 1];
			}
			//clientes_en_mesa.Add(fila_caja[fila_caja.Count() - 1]);
			//fila_caja.RemoveAt(fila_caja.Count - 1);
			foreach (var cliente in fila_caja)
			{
				Point next_step = new Point(cliente.cliente_holder.Location.X, cliente.cliente_holder.Location.Y - cliente.cliente_holder.Size.Height);
				cliente.cliente_holder.Location = next_step;
			}
		}


		private void DeclararMesas(int cantidad)
		{
			Point area_location = new Point(380, 210);
			Point ind_location = area_location;
			int ancho = 900;
			int alto = 380;
			//Cantidad de columnas de mesas
			int col = (int)Math.Ceiling(Math.Sqrt(cantidad));
			int ancho_ind = 60;
			int alto_ind = 60;
			int mesas = 0;
			while (mesas != cantidad)
			{
				for (int columna = 0; columna < col; columna++)
				{
					GenerarMesaHolder(ind_location, ancho_ind, alto_ind, mesas);
					ind_location.X += ancho_ind + ancho / col;
					mesas++;
					if (mesas == cantidad)
						break;
				}
				ind_location.Y += alto_ind;
				ind_location.X = area_location.X;
			}
		}
		private void GenerarMesaHolder(Point locacion, int ancho, int alto, int num_mesa)
		{
			BeginInvoke((MethodInvoker)delegate ()
			{
				PictureBox mesa_holder = new PictureBox();
				mesa_holder.Image = Image.FromFile("table.png");
				mesa_holder.Location = locacion;
				mesa_holder.Width = ancho;
				mesa_holder.Height = alto;
				mesa_holder.SizeMode = PictureBoxSizeMode.Zoom;
				Mesa mesa = new Mesa(mesa_holder, num_mesa);
				mesas.Add(mesa);
				Controls.Add(mesa_holder);
				mesa_holder.BringToFront();
			});

		}
		List<Mesero> meseros = new List<Mesero>();
		private async Task GenerarMeseroPictureBox(string Orden, int n_mesa)
		{
			await Task.Run(() =>
			{
				//switch (Orden.ToLower())
				//{
				//    case "pizza":
				//        IncrementPizzas();
				//        break;
				//    case "boneless":
				//        IncrementBoneless();
				//        break;
				//    case "papas":
				//        IncrementPapas();
				//        break;
				//    case "combo":
				//        IncrementCombos();
				//        break;
				//}
				this.BeginInvoke((MethodInvoker)delegate ()
				{
					PictureBox mesero_holder = new PictureBox();
					mesero_holder.Image = Image.FromFile("waiter.png");
					mesero_holder.Location = counter;
					mesero_holder.Width = 50;
					mesero_holder.Height = 50;
					mesero_holder.SizeMode = PictureBoxSizeMode.Zoom;
					mesero_holder.BringToFront();
					Mesero mesero;

					if (Orden == "Combo")
					{
						PictureBox producto1_holder = new PictureBox();
						producto1_holder.Image = Image.FromFile("pizza.png");
						producto1_holder.Location = new Point(mesero_holder.Location.X + 50, mesero_holder.Location.Y + 20);
						producto1_holder.Width = 30;
						producto1_holder.Height = 30;
						producto1_holder.SizeMode = PictureBoxSizeMode.StretchImage;
						producto1_holder.Tag = Orden;
						producto1_holder.BringToFront();

						PictureBox producto2_holder = new PictureBox();
						producto2_holder.Image = Image.FromFile("boneless.png");
						producto2_holder.Location = new Point(producto1_holder.Location.X + 30, mesero_holder.Location.Y);
						producto2_holder.Width = 30;
						producto2_holder.Height = 30;
						producto2_holder.SizeMode = PictureBoxSizeMode.Zoom;
						producto2_holder.Tag = Orden;
						producto2_holder.BringToFront();

						PictureBox producto3_holder = new PictureBox();
						producto3_holder.Image = Image.FromFile("potatoes.png");
						producto3_holder.Location = new Point(producto2_holder.Location.X + 30, mesero_holder.Location.Y);
						producto3_holder.Width = 30;
						producto3_holder.Height = 30;
						producto3_holder.SizeMode = PictureBoxSizeMode.Zoom;
						producto3_holder.Tag = Orden;
						producto3_holder.BringToFront();

						PictureBox producto4_holder = new PictureBox();
						producto4_holder.Image = Image.FromFile("soft_drink.png");
						producto4_holder.Location = new Point(producto3_holder.Location.X + 30, mesero_holder.Location.Y);
						producto4_holder.Width = 30;
						producto4_holder.Height = 30;
						producto4_holder.SizeMode = PictureBoxSizeMode.Zoom;
						producto4_holder.Tag = Orden;
						producto4_holder.BringToFront();

						mesero = new Mesero(mesero_holder, producto1_holder, producto2_holder, producto3_holder, producto4_holder, n_mesa);
						meseros.Add(mesero);
						mesero.EventoDejarOrden += DejarOrdenEnMesa;
						//Controls.Add(mesero_holder);
						Controls.Add(producto1_holder);
						Controls.Add(producto2_holder);
						Controls.Add(producto3_holder);
						Controls.Add(producto4_holder);
					}
					else
					{
						PictureBox producto1_holder = new PictureBox();
						producto1_holder.Image = Image.FromFile(Orden.ToLower() + ".png");
						producto1_holder.Location = new Point(mesero_holder.Location.X + 10, mesero_holder.Location.Y + 20);
						producto1_holder.Width = 30;
						producto1_holder.Height = 30;
						producto1_holder.SizeMode = PictureBoxSizeMode.Zoom;
						producto1_holder.Tag = Orden;
						producto1_holder.BringToFront();

						mesero = new Mesero(mesero_holder, producto1_holder, n_mesa);
						mesero.EventoDejarOrden += DejarOrdenEnMesa;
						meseros.Add(mesero);
						//Controls.Add(mesero_holder);
						Controls.Add(producto1_holder);
					}
					Task mover_mesero = Task.Run(new Action(() => mesero.MoverPictureBoxAsync(counter, mesas[n_mesa].Mesa_holder.Location)));
					Controls.Add(mesero_holder);
				});
			});

		}
		private int pizzas = 0;
		private int boneless = 0;
		private int papas = 0;
		private int combos = 0;
		private object lock_pizza = new object();
		private object lock_boneless = new object();
		private object lock_papas = new object();
		private object lock_combo = new object();


		public delegate void DelPizza(string i);
		public static event DelPizza EventoPizza;
		public void IncrementPizzas()
		{
			lock (lock_pizza)
			{
				pizzas++;
			}
			EventoPizza(pizzas.ToString());
		}
		public static event DelPizza EventoBoneless;
		public void IncrementBoneless()
		{
			lock (lock_boneless)
			{
				boneless++;
			}
			EventoBoneless(boneless.ToString());
		}
		public static event DelPizza EventoPapas;
		public void IncrementPapas()
		{
			lock (lock_papas)
			{
				papas++;
			}
			EventoPapas(papas.ToString());
		}
		public static event DelPizza EventoCombo;
		public void IncrementCombos()
		{
			lock (lock_combo)
			{
				combos++;
			}
			EventoCombo(combos.ToString());
		}

		private void DejarOrdenEnMesa(int n_mesa, Mesero mesero)
		{
			List<PictureBox> productos = mesero.Elementos_pedido;
			this.Invoke((MethodInvoker)delegate
			{
				mesas[n_mesa].Productos = productos;
				productos[0].Invoke(new Action(() =>
				{
					productos[0].Location = new Point(mesas[n_mesa].Mesa_holder.Location.X, mesas[n_mesa].Mesa_holder.Location.Y);
					productos[0].BringToFront();
				}));
				for (int i = 1; i < productos.Count; i++)
				{
					productos[0].Invoke(new Action(() =>
					{
						productos[i].Location = new Point(productos[i - 1].Location.X + productos[i - 1].Size.Width, productos[i - 1].Location.Y);
						productos[i].BringToFront();
					}));
				}
				meseros.Remove(mesero);
				ClienteCome(mesas[n_mesa].Id_orden);
			});

		}
		private System.Windows.Forms.Timer timer_nuevo_cliente;
		private void timer_nuevo_cliente_Tick(object sender, EventArgs e)
		{
			AdministrarSimulación();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			ComenzarSimulacion();
			ComenzarReloj();
			//while (true)
			//{
			//    //await AdministrarSimulación();
			//    Task Experiencia_ciente = Task.Run(new Action(() =>  AdministrarSimulación()));
			//    Thread.Sleep(tiempo_llegada);
			//}
		}
		public delegate void Notice();
		public static event Notice ComenzarReloj;

		private void ComenzarSimulacion()
		{
			timer_nuevo_cliente = new System.Windows.Forms.Timer();
			timer_nuevo_cliente.Interval = tiempo_llegada;
			timer_nuevo_cliente.Tick += timer_nuevo_cliente_Tick;
			timer_nuevo_cliente.Start();
			AdministrarTv();
			DeclararMesas(cant_mesas);
		}
		//Errors
		//private void DetenerSimulacion()
		//{
		//    timer_nuevo_cliente.Stop();
		//    timer_nuevo_cliente.Dispose();
		//    EliminarClientesEnfila();
		//    EliminarClientesEnMesa();
		//    EliminarMeseros();
		//    EliminarMesas();
		//    cant_ordenes = 0;
		//    clientes_counter = 0;
		//    clientes_in = 0;
		//    clientes_out = 0;
		//    clientes.Clear();
		//}
		private void EliminarClientesEnfila()
		{
			foreach (Cliente cliente in clientes)
			{
				this.Invoke(new Action(() =>
				{
					cliente.cliente_holder.Dispose();
				}));
			}
			fila_caja.Clear();
		}
		private void EliminarClientesEnMesa()
		{
			clientes_en_mesa.Clear();
		}
		private void EliminarMeseros()
		{
			meseros.Clear();
		}
		private void EliminarMesas()
		{
			mesas.Clear();
		}
		private void AdministrarTv()
		{
			Random rng = new Random();
			string[] programas = new string[2] { "football.gif", "box.gif" };
			pb_tv_stream.Image = Image.FromFile(programas[rng.Next(2)]);

		}

		public void EstablecerTiempoLlegada(int mil)
		{
			tiempo_llegada = mil;
		}
		private void EstablecerTiempoOrden(int mil)
		{
			tiempo_orden = mil;
		}
		private void EstablecerTiempoComer(int i, int mil)
		{
			//tiempo_comer = mil;
			tiempos_comer[i] = mil;
		}
		private void EstablecerEmpleadoseEnCaja(int cant)
		{
			empleados_en_caja = cant;
		}
		private void EstablecerTiempoPreparacion(int i, int mil)
		{
			if (i >= 0 && i <= 4)
			{
				tiempos_preparacion[i] = mil;
			}
		}
		int velocidad_tiempo = 1;
		private void EstablecerVelodicadTiempo(int valor)
		{
			//La velocidad se devuelve a uno
			if (velocidad_tiempo > 0)
			{
				tiempo_llegada /= valor;
				//tiempo_comer *= velocidad_tiempo;
				tiempo_orden /= valor;
				for (int i = 0; i < tiempos_preparacion.Length; i++)
				{
					tiempos_preparacion[i] /= valor;
				}
				for (int i = 0; i < tiempos_comer.Length; i++)
				{
					tiempos_comer[i] /= valor;
				}
			}
			//else
			//{
			//    tiempo_llegada /= velocidad_tiempo;
			//    tiempo_comer /= velocidad_tiempo;
			//    tiempo_orden /= velocidad_tiempo;
			//    for (int i = 0; i < tiempos_preparacion.Length; i++)
			//    {
			//        tiempos_preparacion[i] /= velocidad_tiempo;
			//    }
			//}


			//Acelerar
			if (velocidad_tiempo < valor)
			{
				tiempo_llegada *= velocidad_tiempo;
				//tiempo_comer /= velocidad_tiempo;
				tiempo_orden *= velocidad_tiempo;
				for (int i = 0; i < tiempos_preparacion.Length; i++)
				{
					tiempos_preparacion[i] *= velocidad_tiempo;
				}
				for (int i = 0; i < tiempos_comer.Length; i++)
				{
					tiempos_comer[i] *= velocidad_tiempo;
				}

			}
			else if (velocidad_tiempo > valor)
			{
				tiempo_llegada /= velocidad_tiempo;
				//tiempo_comer /= velocidad_tiempo;
				tiempo_orden /= velocidad_tiempo;
				for (int i = 0; i < tiempos_preparacion.Length; i++)
				{
					tiempos_preparacion[i] /= velocidad_tiempo;
				}
				for (int i = 0; i < tiempos_comer.Length; i++)
				{
					tiempos_comer[i] /= velocidad_tiempo;
				}
			}

		}
		private void EstablecerCantidadMesas(int i)
		{
			cant_mesas = i;
		}
		public delegate void DelegadoEnviar(string texto);
		public static event DelegadoEnviar EventoClientesParten;
		public static event DelegadoEnviar EventoClientesLlegan;

		int cant_mesas = 10;
		int clientes_in = 0;
		int clientes_out = 0;
		int tiempo_llegada = 1000;
		int tiempo_orden = 5000;
		//int tiempo_comer=1000;
		int[] tiempos_comer = new int[] { 2000, 1500, 1000, 2000 };
		static int empleados_en_caja = 2;
		List<string> productos = new List<string> { "Pizza", "Boneless", "Potatoes", "Combo" };
		int[] tiempos_preparacion = new int[] { 2000, 1500, 1000, 2500 };


		object obj_lock = new object();
		int cant_ordenes = 0;

		private async Task AdministrarSimulación()
		{
			int id_orden;
			lock (obj_lock)
			{
				cant_ordenes++;
				id_orden = cant_ordenes;
			}
			await GenerarClientePictureBox(id_orden);
			//await Task.Delay(tiempo_orden);
			//await Task.Delay(tiempo_orden + tiempo_orden * (fila_caja.Count()-1));
			//await Task.Delay(tiempo_orden);
			await RealizarPedido();
			List<Cliente> fila_caja_copy = new List<Cliente>(fila_caja);
			MoverAMesa(fila_caja_copy[0]);
			fila_caja.RemoveAt(0);
			//AvanzarFilaCaja();
			//Continua en PrepararOrden
			Random rng = new Random();
			Task preparar_orden = Task.Run(new Action(() => PrepararOrden(id_orden, rng)));
			//if (!IsOrderBeingPrepared(id_orden))
			//{
			//    //await GenerarMeseroPictureBox(ObtenerOrden(rng), mesas[fila_caja_copy[0].Num_mesa].Num_mesa);
			//    await GenerarMeseroPictureBox(ObtenerOrden(rng), mesas[fila_caja_copy[0].Num_mesa].Num_mesa);
			//    MarkOrderAsBeingPrepared(id_orden);
			//}

		}
		SemaphoreSlim caja = new SemaphoreSlim(empleados_en_caja);

		private async Task RealizarPedido()
		{
			await caja.WaitAsync(); // Wait until a slot is available
			try
			{
				await Task.Delay(tiempo_orden); // Simulate the time it takes to make an order
			}
			finally
			{
				caja.Release(); // Release the slot
			}
		}


		private void ClienteCome(int id_orden)
		{
			Task.Run(async () =>
			{
				//await Task.Delay(tiempos_comer);
				foreach (Cliente cliente in clientes_en_mesa)
				{
					if (cliente.id_orden == id_orden)
					{
						foreach (Mesa mesa in mesas)
						{
							if (mesa.Id_orden == cliente.id_orden)
							{
								await Task.Delay(tiempos_comer[productos.IndexOf(mesa.Productos[0].Tag.ToString())]);
								break;
							}
						}
						ClienteParte(cliente);
						EliminarProductos(mesas[cliente.Num_mesa].Productos);
					}
				}
			});

		}
		private async void PrepararOrden(int id_orden, Random rng)
		{
			// Mark the order as being prepared
			//Add a semaphore
			string orden = ObtenerOrden(rng);
			//Thread.Sleep(tiempos_preparacion[productos.IndexOf(orden)]);
			await Task.Delay(tiempos_preparacion[productos.IndexOf(orden)]);
			bool entregado = false;
			while (!entregado)
			{
				foreach (Mesa mesa in mesas)
				{
					if (mesa.Id_orden == id_orden)
					{
						await GenerarMeseroPictureBox(orden, mesas.IndexOf(mesa));
						entregado = true;
					}
					await Task.Delay(100);
				}
			}
		}
		private HashSet<int> ordersBeingPrepared = new HashSet<int>();
		private string ObtenerOrden(Random rng)
		{
			int elegido = rng.Next(101);
			if (elegido <= 5)
			{
				elegido = 3;
				IncrementCombos();
			}
			else if (elegido <= 25)
			{
				elegido = 1;
				IncrementBoneless();
			}
			else if (elegido <= 50)
			{
				elegido = 2;
				IncrementPapas();
			}
			else
			{
				elegido = 0;
				IncrementPizzas();
			}
			return productos[elegido];
		}

		private async void btn_waiter_Click(object sender, EventArgs e)
		{
			await GenerarMeseroPictureBox("Combo", 0);
		}

		Point entrance = new Point(370, 560);
		Point bathroom = new Point(700, 10);
		Point tables = new Point(700, 160);
		Point counter = new Point(10, 160);
		List<Cliente> clientes = new List<Cliente>();
		object lock_cliente = new object();

		private void ActualizarClientesIn()
		{
			lock (lock_cliente)
			{
				clientes_in++;
			}
			EventoClientesLlegan(clientes_in.ToString());
		}

		public delegate void ClientesFila(string i);
		public static event ClientesFila EventoFila;

		int clientes_fila = 0;
		object lock_fila = new object();
		private void IncrementarClientesFila()
		{
			lock (lock_fila)
			{
				clientes_fila++;
			}
			EventoFila(clientes_fila.ToString());
		}

		private async Task<Cliente> GenerarClientePictureBox(int id_orden)
		{
			ActualizarClientesIn();
			IncrementarClientesFila();

			Cliente cliente = null;
			this.BeginInvoke(new Action(async () =>
			{
				PictureBox cliente_holder = new PictureBox();
				cliente_holder.Location = entrance;
				cliente_holder.SizeMode = PictureBoxSizeMode.Zoom;
				Random random = new Random();

				cliente = new Cliente(cliente_holder, clientes);
				cliente.id_orden = id_orden;
				cliente.EventoPartir += ClienteParte;
				clientes.Add(cliente);
				Controls.Add(cliente.cliente_holder);
				fila_caja.Add(cliente);
				cliente_holder.BringToFront();
				int lugar_en_fila = fila_caja.Count() * cliente.cliente_holder.Size.Height;
				Task mover_cliente = Task.Run(new Action(() => cliente.MoverPictureBoxAsync(entrance, new Point(counter.X, counter.Y + lugar_en_fila))));
				//cliente.MoverPictureBoxAsync(entrance, new Point(counter.X, counter.Y + lugar_en_fila));
				//await cliente.MoverPictureBoxAsync(entrance, destino);
			}));
			return cliente;
		}
		object lock_partir = new object();
		public void ActualizarClientesOut()
		{
			lock (lock_partir)
			{
				clientes_out++;
			}
			EventoClientesParten(clientes_out.ToString());
		}
		private void DecrementarClientesMesas()
		{
			lock (lock_mesas)
			{
				clientes_mesas--;
			}
			EventoMesas(clientes_mesas.ToString());
		}

		private async void ClienteParte(Cliente cliente)
		{
			ActualizarClientesOut();
			DecrementarClientesMesas();
			//Someone maybe wipes the table
			Mesa mesa = mesas[cliente.Num_mesa];
			mesa.Disponible = true;
			mesa.Id_orden = 0;
			//clientes_copy.Remove(cliente);
			//clientes_en_mesa_copy.Remove(cliente);
			//foreach (Cliente cliente1 in clientes_en_mesa)
			//{
			//    if (cliente.Num_mesa == 0)
			//    {
			//        EscogerMesa(cliente);
			//    }
			//}
			this.Invoke(new Action(() =>
			{
				cliente.cliente_holder.Dispose(); // Dispose the PictureBox

				// Remove the client from the clientes list
				if (clientes.Contains(cliente))
				{
					clientes.Remove(cliente);
				}

				// Remove the client from the clientes_en_mesa list
				if (clientes_en_mesa.Contains(cliente))
				{
					clientes_en_mesa.Remove(cliente);
				}

			}));

			//Delete client
			//Delete client pb
		}
		int clientes_counter = 0;
		private void btn_bathrom_Click(object sender, EventArgs e)
		{
			if (clientes_counter <= clientes.Count)
			{
				Cliente cliente = clientes[clientes_counter];
				Task mover_cliente = Task.Run(new Action(() => cliente.MoverPictureBoxAsync(cliente.cliente_holder.Location, bathroom)));
				clientes_counter++;
				Caja.Atender();
			}
		}

		private delegate void DelMesas(Cliente cliente);
		private static event DelMesas EventoLLegadaMesas;
		private void btn_mesas_Click(object sender, EventArgs e)
		{
			//DetenerSimulacion();
			//MoverAMesa();
			//GenerarClientePictureBox(1);
		}


		public static event ClientesFila EventoMesas;
		int clientes_mesas;
		object lock_mesas = new object();

		private void IncrementarClientesMesa()
		{
			lock (lock_mesas)
			{
				clientes_mesas++;
			}
			EventoMesas(clientes_mesas.ToString());
		}
		private void DecrementarClientesFila()
		{
			lock (lock_fila)
			{
				clientes_fila--;
			}
			EventoFila(clientes_fila.ToString());
		}

		int totalMesas = 0;

		private void MoverAMesa(Cliente cliente)
		{
			IncrementarClientesMesa();
			DecrementarClientesFila();
			//Cliente cliente = clientes[clientes_counter];
			clientes_en_mesa.Add(cliente);
			Task mover_cliente = Task.Run(async () =>
			{
				await cliente.MoverPictureBoxAsync(cliente.cliente_holder.Location, tables);
				//EventoLLegadaMesas(cliente);
				EscogerMesa(cliente);
			}
			);
			clientes_counter++;
		}
		private async void EscogerMesa(Cliente cliente)
		{
			List<Mesa> mesas_copy = new List<Mesa>(mesas);
			bool sin_mesa = true;
			while (sin_mesa)
			{
				foreach (var mesa in mesas_copy)
				{
					if (mesa.Disponible == true)
					{
						mesa.Disponible = false;
						cliente.Num_mesa = mesa.Num_mesa;
						mesa.Id_orden = cliente.id_orden;
						await cliente.MoverPictureBoxAsync(cliente.cliente_holder.Location, new Point(mesa.Mesa_holder.Location.X - 30, mesa.Mesa_holder.Location.Y));
						sin_mesa = false;
						break;
					}
				}

				await Task.Delay(100);
			}
		}

		private void btn_cliente_leaves_Click(object sender, EventArgs e)
		{
			//EliminarProductos(mesas[ clientes_en_mesa[0].Num_mesa].Productos);
			//Avanzar fila like method for tables
			//ClienteParte(clientes_en_mesa[0]);
		}
		private async void EliminarProductos(List<PictureBox> productos)
		{
			//List<PictureBox> productos_copy = new List<PictureBox>(productos);
			//foreach (var pictureBox in productos_copy)
			//{
			//    await Task.Delay(100);
			//    if (pictureBox != null && pictureBox.Parent != null)
			//    {
			//        pictureBox.Parent.BeginInvoke(new Action(() =>
			//        {
			//            pictureBox.Parent.Controls.Remove(pictureBox);
			//            pictureBox.Dispose();
			//        }));
			//    }
			//}
			List<PictureBox> productos_copy = new List<PictureBox>(productos);
			foreach (var pictureBox in productos_copy)
			{
				await Task.Delay(100);
				if (pictureBox != null && pictureBox.Parent != null)
				{
					pictureBox.Parent.BeginInvoke(new Action(() =>
					{
						if (pictureBox.Parent != null && pictureBox.Parent.Controls.Contains(pictureBox))
						{
							pictureBox.Parent.Controls.Remove(pictureBox);
						}
						pictureBox.Dispose();
					}));
				}
			}

		}
		int music_ind = 0;
		string[] music_tracks = {
			"C:\\Users\\HUAWEI\\Downloads\\parararararaaaaaan pararararaaaan (spider-man 3 Black suit theme).wav",
			"C:\\Users\\HUAWEI\\Downloads\\Spider-Man 2： The Game Pizza Theme.wav"
		};

		SoundPlayer sp;
		private void btn_jukebox_Click(object sender, EventArgs e)
		{
			CambiarMusica();
			music_ind++;
		}


		private void CambiarMusica()
		{
			if (music_ind == music_tracks.Length)
			{
				music_ind = 0;
				sp.Stop();
			}
			else
			{
				sp = new SoundPlayer(music_tracks[music_ind]);
				sp.Play();
			}
		}


	}
	class Cliente
	{
		public static List<Cliente> clientes;
		public PictureBox cliente_holder;
		private Random rng;
		private int num_mesa;
		public delegate void delPartir(Cliente cliente);
		public event delPartir EventoPartir;
		public int id_orden;
		public Cliente(PictureBox cliente_holder, List<Cliente> clientes_)
		{
			clientes = clientes_;
			rng = new Random();
			cliente_holder.Image = Image.FromFile("customer" + rng.Next(10) + ".gif");
			cliente_holder.Width = 50;
			cliente_holder.Height = 50;
			this.cliente_holder = cliente_holder;
		}

		public int Num_mesa { get => num_mesa; set => num_mesa = value; }

		public virtual async Task MoverPictureBoxAsync(Point startPoint, Point endPoint)
		{
			if (cliente_holder.Location.X > endPoint.X)
			{
				while (cliente_holder.Location.X > endPoint.X)
				{
					Point next_step = new Point(cliente_holder.Location.X - 10, cliente_holder.Location.Y);
					await MovePictureBoxAsync(next_step, 10);
				}
			}
			else
			{
				while (cliente_holder.Location.X < endPoint.X)
				{
					Point next_step = new Point(cliente_holder.Location.X + 10, cliente_holder.Location.Y);
					await MovePictureBoxAsync(next_step, 10);

				}
			}
			if (cliente_holder.Location.Y > endPoint.Y)
			{
				while (cliente_holder.Location.Y > endPoint.Y)
				{
					Point next_step = new Point(cliente_holder.Location.X, cliente_holder.Location.Y - 10);
					await MovePictureBoxAsync(next_step, 10);

				}
			}
			else
			{
				while (cliente_holder.Location.Y < endPoint.Y)
				{
					Point next_step = new Point(cliente_holder.Location.X, cliente_holder.Location.Y + 10);
					await MovePictureBoxAsync(next_step, 10);

				}
			}
		}


		private async Task MovePictureBoxAsync(Point nextStep, int delayMilliseconds)
		{
			cliente_holder.Invoke(new Action(() =>
			{
				try
				{
					cliente_holder.Location = nextStep;
					Task.Delay(100);
				}
				catch (Exception)
				{
					return;
				}
			}));

			await Task.Delay(delayMilliseconds);
		}
		public void Comer(int tiempo_mili)
		{
			Thread.Sleep(tiempo_mili);
			EventoPartir(this);
		}
	}

	class Mesa
	{

		PictureBox mesa_holder;
		bool disponible = true;
		private int num_mesa;
		List<PictureBox> productos = new List<PictureBox>();
		int id_orden;

		public Mesa(PictureBox mesa_holder, int num_mesa)
		{
			this.Mesa_holder = mesa_holder;
			this.num_mesa = num_mesa;
		}

		public PictureBox Mesa_holder { get => mesa_holder; set => mesa_holder = value; }
		public bool Disponible { get => disponible; set => disponible = value; }
		public int Num_mesa { get => num_mesa; set => num_mesa = value; }
		public List<PictureBox> Productos { get => productos; set => productos = value; }
		public int Id_orden { get => id_orden; set => id_orden = value; }
	}
	class Caja
	{
		public delegate void Delcliente();
		public static event Delcliente EventoClienteAtendido;
		private static int Tiempo_atender_en_caja = 0;
		public static void Atender()
		{
			Thread.Sleep(Tiempo_atender_en_caja);
			EventoClienteAtendido();
		}
	}

	class Mesero : IDisposable
	{
		public delegate void DejarOrden(int n_mesa, Mesero mesero);
		public event DejarOrden EventoDejarOrden;
		List<PictureBox> elementos_pedido = new List<PictureBox>();
		PictureBox mesero_holder;
		int n_mesa;

		public List<PictureBox> Elementos_pedido { get => elementos_pedido; set => elementos_pedido = value; }

		public Mesero(PictureBox mesero_holder, PictureBox producto_holder, int n_mesa)
		{
			this.mesero_holder = mesero_holder;
			Elementos_pedido.Add(producto_holder);
			this.n_mesa = n_mesa;
		}
		public void DejarProductos()
		{
			EventoDejarOrden(n_mesa, this);
		}
		public Mesero(PictureBox mesero_holder, PictureBox producto_holder1, PictureBox producto_holder2, PictureBox producto_holder3, PictureBox producto_holder4, int n_mesa)
		{
			this.mesero_holder = mesero_holder;
			Elementos_pedido.Add(producto_holder1);
			Elementos_pedido.Add(producto_holder2);
			Elementos_pedido.Add(producto_holder3);
			Elementos_pedido.Add(producto_holder4);
			this.n_mesa = n_mesa;
		}
		public virtual async Task MoverPictureBoxAsync(Point startPoint, Point endPoint)
		{
			if (mesero_holder.Location.X > endPoint.X)
			{
				while (mesero_holder.Location.X > endPoint.X)
				{
					Point next_step = new Point(mesero_holder.Location.X - 10, mesero_holder.Location.Y);
					await MovePictureBoxAsync(next_step, 10);
					foreach (PictureBox producto in Elementos_pedido)
					{
						producto.BeginInvoke(new Action(async () =>
						{
							producto.Location = new Point(producto.Location.X - 10, producto.Location.Y);
							await Task.Delay(100);
						}));
					}
				}
			}
			else
			{
				while (mesero_holder.Location.X < endPoint.X)
				{
					Point next_step = new Point(mesero_holder.Location.X + 10, mesero_holder.Location.Y);
					await MovePictureBoxAsync(next_step, 10);
					foreach (PictureBox producto in Elementos_pedido)
					{
						producto.BeginInvoke(new Action(async () =>
						{
							producto.Location = new Point(producto.Location.X + 10, producto.Location.Y);
							await Task.Delay(100);

						}));
					}

				}
			}
			if (mesero_holder.Location.Y > endPoint.Y)
			{
				while (mesero_holder.Location.Y > endPoint.Y)
				{
					Point next_step = new Point(mesero_holder.Location.X, mesero_holder.Location.Y - 10);
					await MovePictureBoxAsync(next_step, 10);
					foreach (PictureBox producto in Elementos_pedido)
					{
						producto.BeginInvoke(new Action(async () =>
						{
							producto.Location = new Point(producto.Location.X, producto.Location.Y - 10);
							await Task.Delay(100);
						}));
					}
				}
			}
			else
			{
				while (mesero_holder.Location.Y < endPoint.Y)
				{
					Point next_step = new Point(mesero_holder.Location.X, mesero_holder.Location.Y + 10);
					await MovePictureBoxAsync(next_step, 10);
					foreach (PictureBox producto in Elementos_pedido)
					{
						producto.BeginInvoke(new Action(async () =>
						{
							producto.Location = new Point(producto.Location.X, producto.Location.Y + 10);
							await Task.Delay(100);
						}));
					}
				}
			}
			DejarProductos();
		}
		private async Task MovePictureBoxAsync(Point nextStep, int delayMilliseconds)
		{
			mesero_holder.Invoke(new Action(() =>
			{
				mesero_holder.Location = nextStep;
			}));

			await Task.Delay(delayMilliseconds);
		}
		public void Dispose()
		{
			foreach (PictureBox producto in Elementos_pedido)
			{
				producto.Dispose();
			}
			mesero_holder.Dispose();
		}
	}
}

