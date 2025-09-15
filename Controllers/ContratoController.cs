using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;

    private ContratoRepository repository;
    private InmuebleRepository inmuebleRrepository;
    private InquilinoRepository inquilinoRrepository;
    private PropietarioRepository propietarioRepository;
    public ContratoController(ILogger<ContratoController> logger)
    {
        _logger = logger;
        repository = new ContratoRepository();
        inmuebleRrepository = new InmuebleRepository();
        inquilinoRrepository = new InquilinoRepository();
        propietarioRepository = new PropietarioRepository();
    }

    public IActionResult Index()
    {
        var contratos = repository.GetAll();
        return View(contratos);
    }

    public IActionResult Create()
    {
        var inmuebles = inmuebleRrepository.GetAll();
        ViewBag.Inmuebles = new SelectList(inmuebles, "Id", "Direccion");
        var inquilinos = inquilinoRrepository.GetAll();
        ViewBag.Inquilinos = new SelectList(inquilinos, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contrato contrato)
    {
        repository.InsertContrato(contrato);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var inmuebles = inmuebleRrepository.GetAll();
        ViewBag.Inmuebles = new SelectList(inmuebles, "Id", "Direccion");
        var inquilinos = inquilinoRrepository.GetAll();
        ViewBag.Inquilinos = new SelectList(inquilinos, "Id", "Dni");
        if (id == 0) return View();
        var contrato = repository.GetContratoById(id);
        return View(contrato);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Contrato contrato)
    {
        repository.EditContrato(contrato);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var contrato = repository.GetContratoById(id);
        var inmueble = inmuebleRrepository.GetInmuebleById(contrato.Contrato_inmueble);
        ViewBag.Inmueble = inmueble;
        var inquilino = inquilinoRrepository.GetInquilinoById(contrato.Contrato_inquilino);
        ViewBag.Inquilino = inquilino;
        var propietario = propietarioRepository.GetPropietarioById(inmueble.Inmueble_propietario);
        ViewBag.Propietario = propietario;
        return View(contrato);
    }
    public IActionResult Delete(int id)
    {
        var contrato = repository.GetContratoById(id);
        return View(contrato);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        repository.DeleteContrato(id);
        return RedirectToAction(nameof(Index));
    }
}