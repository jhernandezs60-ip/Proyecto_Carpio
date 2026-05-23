namespace SistemaVentasVendedor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lstVendedores = new System.Windows.Forms.ListBox();
            this.lblResumen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLista = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // Título principal
            this.lblTitulo.Text = "💼 SISTEMA DE CONTROL DE VENTAS";
            this.lblTitulo.Location = new System.Drawing.Point(0, 15);
            this.lblTitulo.Size = new System.Drawing.Size(700, 35);
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Label Nombre
            this.label1.Text = "Nombre del Vendedor:";
            this.label1.Location = new System.Drawing.Point(30, 75);
            this.label1.Size = new System.Drawing.Size(160, 22);
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);

            // TextBox Nombre
            this.txtNombre.Location = new System.Drawing.Point(200, 72);
            this.txtNombre.Size = new System.Drawing.Size(250, 26);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);
            this.txtNombre.ForeColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Label Monto
            this.label2.Text = "Monto de Ventas (Q):";
            this.label2.Location = new System.Drawing.Point(30, 115);
            this.label2.Size = new System.Drawing.Size(160, 22);
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);

            // TextBox Monto
            this.txtMonto.Location = new System.Drawing.Point(200, 112);
            this.txtMonto.Size = new System.Drawing.Size(250, 26);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMonto.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);
            this.txtMonto.ForeColor = System.Drawing.Color.White;
            this.txtMonto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Botón Registrar
            this.btnRegistrar.Text = "✔ REGISTRAR VENTA";
            this.btnRegistrar.Location = new System.Drawing.Point(200, 155);
            this.btnRegistrar.Size = new System.Drawing.Size(180, 38);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(0, 150, 136);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);

            // Label Lista
            this.lblLista.Text = "📋 REGISTRO DE VENTAS";
            this.lblLista.Location = new System.Drawing.Point(30, 210);
            this.lblLista.Size = new System.Drawing.Size(640, 22);
            this.lblLista.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLista.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);

            // ListBox
            this.lstVendedores.Location = new System.Drawing.Point(30, 235);
            this.lstVendedores.Size = new System.Drawing.Size(640, 180);
            this.lstVendedores.Name = "lstVendedores";
            this.lstVendedores.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstVendedores.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            this.lstVendedores.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.lstVendedores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Label Resumen
            this.lblResumen.Text = "📊 Resumen aparecerá aquí";
            this.lblResumen.Location = new System.Drawing.Point(30, 428);
            this.lblResumen.Size = new System.Drawing.Size(640, 50);
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
            this.lblResumen.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblResumen.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);

            // Form
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.Text = "Sistema de Control de Ventas por Vendedor";
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblLista);
            this.Controls.Add(this.lstVendedores);
            this.Controls.Add(this.lblResumen);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.ListBox lstVendedores;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLista;
    }
}