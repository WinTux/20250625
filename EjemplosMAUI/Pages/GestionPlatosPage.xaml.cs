using EjemplosMAUI.Conexion;
using EjemplosMAUI.Models;

namespace EjemplosMAUI.Pages;
[QueryProperty(nameof(plato),"Plato")]
public partial class GestionPlatosPage : ContentPage
{
	private readonly IRestConexionDatos _restConexionDatos;
	private Plato _plato;
	public Plato plato
	{
		get => _plato;
		set
		{
			_esNuevoPlato = esNuevo(value);
			_plato = value;
            OnPropertyChanged();//Obliga a actualizar los enlaces de datos
        }
    }
    private bool _esNuevoPlato;//Para controlar si es un plato nuevo o uno existente
    public GestionPlatosPage(IRestConexionDatos restConexionDatos)
	{
		InitializeComponent();
		_restConexionDatos = restConexionDatos;
		BindingContext = this;//Establecemos el contexto de enlace para que los controles puedan acceder a las propiedades de la página
    }

	private bool esNuevo(Plato plato) {
		if (plato.Id == 0)
			return true;
		return false;
    }

	async void OnCancelarClic(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");//Volver a la página anterior
    }
	async void OnGuardarPlato(object sender, EventArgs e) {
		if (_esNuevoPlato) {
			await _restConexionDatos.AddPltoAsync(plato);
		} else {
			await _restConexionDatos.UpdatePlatoAsync(plato);
		}
		await Shell.Current.GoToAsync("..");//Volver a la página anterior
    }

	async void OnEliminarPlato(object sender, EventArgs e)
	{ 
		await _restConexionDatos.DeletePlatoAsync(plato.Id);
        await Shell.Current.GoToAsync("..");//Volver a la página anterior
    }
}