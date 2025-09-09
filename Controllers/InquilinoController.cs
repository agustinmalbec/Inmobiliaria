using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;

namespace Inmobiliaria.Controllers;

public class InquilinoController : Controller
{
    private readonly ILogger<InquilinoController> _logger;
    private InquilinoRepository repository;
    public InquilinoController(ILogger<InquilinoController> logger)
    {
        _logger = logger;
        repository = new InquilinoRepository();
    }

    public IActionResult Index()
    {
        var inquilinos = repository.GetAll();
        return View(inquilinos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        repository.InsertInquilino(inquilino);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        if (id == 0) return View();
        var inquilino = repository.GetInquilinoById(id);
        return View(inquilino);
    }

    [HttpPost]
    public IActionResult Edit(Inquilino inquilino)
    {
        repository.EditInquilino(inquilino);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var inquilino = repository.GetInquilinoById(id);
        return View(inquilino);
    }
    public IActionResult Delete(int id)
    {
        var inquilino = repository.GetInquilinoById(id);
        return View(inquilino);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        repository.DeleteInquilino(id);
        return RedirectToAction(nameof(Index));
    }
}
