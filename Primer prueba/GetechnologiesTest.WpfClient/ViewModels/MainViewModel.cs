using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GetechnologiesTest.WpfClient.Services;

namespace GetechnologiesTest.WpfClient.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ApiClient _api;

    public MainViewModel(ApiClient api)
    {
        _api = api;
        RegistrarCommand = new RelayCommand(async _ => await RegistrarAsync(), _ => PuedeRegistrar);
        ValidarTodo();
        RegistrarFacturaCommand = new RelayCommand(async _ => await RegistrarFacturaAsync(), _ => PuedeRegistrarFactura);
        ValidarFactura();

    }

    public ICommand RegistrarFacturaCommand { get; }

    // Campos
    private string _nombre = "";
    public string Nombre
    {
        get => _nombre;
        set { _nombre = value; OnPropertyChanged(); ValidarTodo(); }
    }

    private string _apellidoPaterno = "";
    public string ApellidoPaterno
    {
        get => _apellidoPaterno;
        set { _apellidoPaterno = value; OnPropertyChanged(); ValidarTodo(); }
    }

    private string? _apellidoMaterno;
    public string? ApellidoMaterno
    {
        get => _apellidoMaterno;
        set { _apellidoMaterno = value; OnPropertyChanged(); }
    }

    private string _identificacion = "";
    public string Identificacion
    {
        get => _identificacion;
        set { _identificacion = value; OnPropertyChanged(); ValidarTodo(); }
    }

    public bool PuedeRegistrarFactura =>
    string.IsNullOrWhiteSpace(ErrorFacturaPersonaId) &&
    string.IsNullOrWhiteSpace(ErrorFacturaMonto) &&
    !string.IsNullOrWhiteSpace(FacturaPersonaId) &&
    !string.IsNullOrWhiteSpace(FacturaMonto) &&
    !IsBusy;

    // Errores
    private string? _errorNombre;
    public string? ErrorNombre { get => _errorNombre; private set { _errorNombre = value; OnPropertyChanged(); } }

    private string? _errorApellidoPaterno;
    public string? ErrorApellidoPaterno { get => _errorApellidoPaterno; private set { _errorApellidoPaterno = value; OnPropertyChanged(); } }

    private string? _errorIdentificacion;
    public string? ErrorIdentificacion { get => _errorIdentificacion; private set { _errorIdentificacion = value; OnPropertyChanged(); } }

    private string? _status;
    public string? Status { get => _status; private set { _status = value; OnPropertyChanged(); } }

    private string? _errorFacturaPersonaId;
    public string? ErrorFacturaPersonaId
    {
        get => _errorFacturaPersonaId;
        private set { _errorFacturaPersonaId = value; OnPropertyChanged(); }
    }

    private string? _errorFacturaMonto;
    public string? ErrorFacturaMonto
    {
        get => _errorFacturaMonto;
        private set { _errorFacturaMonto = value; OnPropertyChanged(); }
    }

    private string? _statusFactura;
    public string? StatusFactura
    {
        get => _statusFactura;
        private set { _statusFactura = value; OnPropertyChanged(); }
    }

    public bool PuedeRegistrar =>
        string.IsNullOrWhiteSpace(ErrorNombre) &&
        string.IsNullOrWhiteSpace(ErrorApellidoPaterno) &&
        string.IsNullOrWhiteSpace(ErrorIdentificacion) &&
        !string.IsNullOrWhiteSpace(Nombre) &&
        !string.IsNullOrWhiteSpace(ApellidoPaterno) &&
        !string.IsNullOrWhiteSpace(Identificacion) &&
        !IsBusy;

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        private set { _isBusy = value; OnPropertyChanged(); OnPropertyChanged(nameof(PuedeRegistrar)); }
    }

    public ICommand RegistrarCommand { get; }

    private void ValidarTodo()
    {
        ErrorNombre = string.IsNullOrWhiteSpace(Nombre) ? "Nombre es obligatorio." : null;
        ErrorApellidoPaterno = string.IsNullOrWhiteSpace(ApellidoPaterno) ? "Apellido Paterno es obligatorio." : null;
        ErrorIdentificacion = string.IsNullOrWhiteSpace(Identificacion) ? "Identificación es obligatoria." : null;

        (RegistrarCommand as RelayCommand)?.RaiseCanExecuteChanged();
        OnPropertyChanged(nameof(PuedeRegistrar));
    }



    // Métodos
    private async Task RegistrarAsync()
    {
        try
        {
            IsBusy = true;
            Status = "Registrando...";

            var req = new
            {
                nombre = Nombre.Trim(),
                apellidoPaterno = ApellidoPaterno.Trim(),
                apellidoMaterno = string.IsNullOrWhiteSpace(ApellidoMaterno) ? null : ApellidoMaterno.Trim(),
                identificacion = Identificacion.Trim()
            };

            var id = await _api.CrearPersonaAsync(req);

            Status = $" Persona registrada con ID: {id}";
            // Limpia formulario
            Nombre = "";
            ApellidoPaterno = "";
            ApellidoMaterno = "";
            Identificacion = "";
        }
        catch (Exception ex)
        {
            Status = $"❌ Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            (RegistrarCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }
    private async Task RegistrarFacturaAsync()
    {
        try
        {
            IsBusy = true;
            StatusFactura = "Registrando factura...";

            var personaId = int.Parse(FacturaPersonaId);
            var monto = decimal.Parse(FacturaMonto);

            var req = new
            {
                personaId = personaId,
                monto = monto,
                fecha = FacturaFecha // si es null, API pone DateTime.UtcNow
            };

            var id = await _api.CrearFacturaAsync(req);

            StatusFactura = $" Factura registrada con ID: {id}";

            // Limpiar form
            FacturaPersonaId = "";
            FacturaMonto = "";
            FacturaFecha = null;
        }
        catch (Exception ex)
        {
            StatusFactura = $"❌ Error: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
            (RegistrarFacturaCommand as RelayCommand)?.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(PuedeRegistrarFactura));
        }
    }

    // INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    private string _facturaPersonaId = "";
    public string FacturaPersonaId
    {
        get => _facturaPersonaId;
        set { _facturaPersonaId = value; OnPropertyChanged(); ValidarFactura(); }
    }

    private string _facturaMonto = "";
    public string FacturaMonto
    {
        get => _facturaMonto;
        set { _facturaMonto = value; OnPropertyChanged(); ValidarFactura(); }
    }

    private DateTime? _facturaFecha;
    public DateTime? FacturaFecha
    {
        get => _facturaFecha;
        set { _facturaFecha = value; OnPropertyChanged(); }
    }

    private void ValidarFactura()
    {
        // PersonaId
        if (!int.TryParse(FacturaPersonaId, out var pid) || pid <= 0)
            ErrorFacturaPersonaId = "PersonaId inválido (debe ser entero > 0).";
        else
            ErrorFacturaPersonaId = null;

        // Monto
        if (!decimal.TryParse(FacturaMonto, out var monto) || monto <= 0)
            ErrorFacturaMonto = "Monto inválido (debe ser mayor a 0).";
        else
            ErrorFacturaMonto = null;

        (RegistrarFacturaCommand as RelayCommand)?.RaiseCanExecuteChanged();
        OnPropertyChanged(nameof(PuedeRegistrarFactura));
    }

}
