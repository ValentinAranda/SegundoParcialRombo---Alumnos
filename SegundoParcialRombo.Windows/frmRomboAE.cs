using SegundoParcialRombo.Datos;
using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Windows
{
    public partial class frmRomboAE : Form
    {
        private Rombo? rombo;
        private readonly RepositorioRombo? _repo;
        public frmRomboAE(RepositorioRombo? repo)
        {
            InitializeComponent();
            _repo = repo;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (rombo != null)
            {
                txtDiagonalMayor.Text = rombo.DiagonalMayor.ToString();
                txtDiagonalMenor.Text = rombo.DiagonalMenor.ToString();
                switch (rombo.Tipocontorno)
                {
                    case Contorno.solido:
                        rbtSolido.Checked = true;
                        break;
                    case Contorno.punteado:
                        rbtPunteado.Checked = true;
                        break;
                    case Contorno.rayado:
                        rbtRayado.Checked = true;
                        break;
                    case Contorno.doble:
                        rbtDoble.Checked = true;
                        break;
                }
            }
        }
        public Rombo? GetRombo()
        {
            return rombo;
        }

        public void SetRombo(Rombo rombo)
        {
            this.rombo = rombo;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (rombo is null)
                {
                    rombo = new Rombo();
                }
                rombo.DiagonalMayor=int.Parse(txtDiagonalMayor.Text);
                rombo.DiagonalMenor = int.Parse(txtDiagonalMenor.Text);
                DialogResult = DialogResult.OK;

            }
        }
        private bool ValidarDatos()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!int.TryParse(txtDiagonalMayor.Text, out int Dma) ||
                Dma<=0)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMayor, "Diagonal Mayor mal ingresada");
            }
            if (!int.TryParse(txtDiagonalMenor.Text, out int Dme) ||
                Dme <= 0 || Dme>=Dma)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMenor, "Diagonal Menor mal ingresada");
            }
            if (_repo!.Existe(Dma, Dme))
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMayor, "¡¡El rombo ingresado ya existe!!");
            }
            return valido;
        }


    }
}
