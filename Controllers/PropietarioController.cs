using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class PropietarioController : Controller
{
    private readonly ILogger<PropietarioController> _logger;
    private PropietarioRepository repository;
    public PropietarioController(ILogger<PropietarioController> logger)
    {
        _logger = logger;
        repository = new PropietarioRepository();
    }

    public IActionResult Index()
    {
        var propietarios = repository.GetAll();
        return View(propietarios);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Propietario propietario)
    {
        repository.InsertPropietario(propietario);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        if (id == 0) return View();
        var propietario = repository.GetPropietarioById(id);
        return View(propietario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Propietario propietario)
    {
        repository.EditPropietario(propietario);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var propietario = repository.GetPropietarioById(id);
        return View(propietario);
    }
    public IActionResult Delete(int id)
    {
        var propietario = repository.GetPropietarioById(id);
        return View(propietario);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        repository.DeletePropietario(id);
        return RedirectToAction(nameof(Index));
    }
}