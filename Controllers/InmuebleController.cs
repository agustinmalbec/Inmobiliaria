using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inmobiliaria.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private InmuebleRepository repository;
    private PropietarioRepository propietarioRepository;
    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
        repository = new InmuebleRepository();
        propietarioRepository = new PropietarioRepository();
    }

    public IActionResult Index()
    {
        var inmuebles = repository.GetAll();
        return View(inmuebles);
    }

    public IActionResult Create()
    {
        var propietarios = propietarioRepository.GetAll();
        ViewBag.Propietarios = new SelectList(propietarios, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Inmueble inmueble)
    {
        repository.InsertInmueble(inmueble);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        if (id == 0) return View();
        var inmueble = repository.GetInmuebleById(id);
        var propietarios = propietarioRepository.GetAll();
        ViewBag.Propietarios = new SelectList(propietarios, "Id", "Nombre");
        return View(inmueble);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Inmueble inmueble)
    {
        repository.EditInmueble(inmueble);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var inmueble = repository.GetInmuebleById(id);
        var propietario = propietarioRepository.GetPropietarioById(inmueble.Inmueble_propietario);
        ViewBag.Propietario = propietario;
        return View(inmueble);
    }
    public IActionResult Delete(int id)
    {
        var inmueble = repository.GetInmuebleById(id);
        return View(inmueble);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        repository.DeleteInmueble(id);
        return RedirectToAction(nameof(Index));
    }
}