using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace GestorTaller
{
    /// <summary>
    /// Campo de texto con un icono a la izquierda y borde redondeado; el borde se engrosa y oscurece
    /// cuando el campo tiene el foco. Contiene un TextBox sin borde (<see cref="Entrada"/>), que se
    /// puede usar para eventos como KeyDown. Con <see cref="EsContrasena"/> oculta lo que se escribe.
    /// </summary>
    public class CampoConIcono : Panel
    {
        private readonly TextBox _entrada = new()
        {
            BorderStyle = BorderStyle.None,
            Font = Tema.Fuente(11F)
        };

        private string _nombreIcono = string.Empty;

        public CampoConIcono()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Tema.Blanco;
            _entrada.BackColor = Tema.Blanco;
            _entrada.ForeColor = Tema.Titanio;
            _entrada.GotFocus += (_, _) => Invalidate();
            _entrada.LostFocus += (_, _) => Invalidate();
            Controls.Add(_entrada);
        }

        /// <summary>El cuadro de texto interno, para engancharle eventos.</summary>
        [Browsable(false)]
        public TextBox Entrada => _entrada;

        /// <summary>Texto escrito en el campo.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Texto
        {
            get => _entrada.Text;
            set => _entrada.Text = value;
        }

        /// <summary>Nombre de un icono de Recursos\Iconos (por ejemplo "user"); vacío = sin icono.</summary>
        [DefaultValue("")]
        public string NombreIcono
        {
            get => _nombreIcono;
            set { _nombreIcono = value ?? string.Empty; Invalidate(); }
        }

        /// <summary>True para ocultar lo que se escribe (contraseñas).</summary>
        [DefaultValue(false)]
        public bool EsContrasena
        {
            get => _entrada.UseSystemPasswordChar;
            set => _entrada.UseSystemPasswordChar = value;
        }

        [DefaultValue(32767)]
        public int MaxLength
        {
            get => _entrada.MaxLength;
            set => _entrada.MaxLength = value;
        }

        protected override Size DefaultSize => new(320, 44);

        /// <summary>Pone el foco en el cuadro de texto.</summary>
        public new bool Focus() => _entrada.Focus();

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            var izquierda = Escala(_nombreIcono.Length > 0 ? 46 : 14);
            _entrada.SetBounds(izquierda, (Height - _entrada.Height) / 2, Math.Max(Width - izquierda - Escala(14), 1), _entrada.Height);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _entrada.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent is { BackColor.A: > 0 } ? Parent.BackColor : Tema.Fondo);

            var enfocado = _entrada.Focused;
            var grosor = enfocado ? 2 : 1;
            var area = new Rectangle(grosor / 2, grosor / 2, Width - grosor, Height - grosor);
            using var ruta = Dibujo.RectanguloRedondeado(area, Escala(Tema.RadioControl));
            using var pincel = new SolidBrush(Tema.Blanco);
            using var lapiz = new Pen(enfocado ? Tema.Pizarra : Tema.BordeCampo, grosor);
            g.FillPath(pincel, ruta);
            g.DrawPath(lapiz, ruta);

            if (_nombreIcono.Length > 0)
            {
                var tamano = Escala(20);
                g.DrawImage(Iconos.Obtener(_nombreIcono, tamano, Tema.TextoSecundario), Escala(14), (Height - tamano) / 2, tamano, tamano);
            }
        }

        private int Escala(int valor) => Tema.Escalar(valor, DeviceDpi);
    }
}
