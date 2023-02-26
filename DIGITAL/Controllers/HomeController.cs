using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DIGITAL.Models;
using DIGITAL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NPOI.XSSF.UserModel; //para trabajar con archivos xlsx


namespace DIGITAL.Controllers {
    public class HomeController : Controller {

        private readonly DbempleadosContext _DBcontext;

        public HomeController(DbempleadosContext context) {
            _DBcontext = context;
        }

        public IActionResult Index(int idEmpleadoX = 0) {
            //List<Empleado> lista = _DBcontext.Empleados.Include(c => c.oCargo).ToList();
            //return View(lista);

            if (idEmpleadoX == 0) {
                
                var clientes = from c in _DBcontext.Empleados.Include(c => c.oCargo)
                               select c;
                return View(clientes.ToList());

            } else {
                var clientes = from c in _DBcontext.Empleados.Include(c => c.oCargo)
                               where c.IdEmpleado == idEmpleadoX
                               select c;

                return View(clientes.ToList());
            }
            
        }

        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado) {

            EmpleadoVM oEmpleadoVM = new EmpleadoVM() {
                oEmpleado = new Empleado(),
                oListaCargo = _DBcontext.Cargos.Select(cargo => new SelectListItem() {
                    Text = cargo.Descripcion,
                    Value = cargo.IdCargo.ToString()

                }).ToList()
            };

            if (idEmpleado != 0) {

                oEmpleadoVM.oEmpleado = _DBcontext.Empleados.Find(idEmpleado);
            }


            return View(oEmpleadoVM);
        }

        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVM oEmpleadoVM) {
            if (oEmpleadoVM.oEmpleado.IdEmpleado == 0) {
                _DBcontext.Empleados.Add(oEmpleadoVM.oEmpleado);

            } else {
                _DBcontext.Empleados.Update(oEmpleadoVM.oEmpleado);
            }

            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Eliminar(int idEmpleado) {
            Empleado oEmpleado = _DBcontext.Empleados.Include(c => c.oCargo).Where(e => e.IdEmpleado == idEmpleado).FirstOrDefault();

            return View(oEmpleado);
        }

        [HttpPost]
        public IActionResult Eliminar(Empleado oEmpleado) {
            _DBcontext.Empleados.Remove(oEmpleado);
            _DBcontext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        public ActionResult GenerateExcel() {
            //crear libro de trabajo
            XSSFWorkbook workbook = new XSSFWorkbook();

            //crear hoja de trabajo
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Empleados");

            //crear fila de encabezado
            XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Nombre completo");
            headerRow.CreateCell(1).SetCellValue("Correo");
            headerRow.CreateCell(2).SetCellValue("Telefono");
            headerRow.CreateCell(3).SetCellValue("Cargo");

            List<Empleado> empleados = _DBcontext.Empleados.Include(c => c.oCargo).ToList();
            
            int rowNum = 1;

            foreach (Empleado empleado in empleados) {
                XSSFRow row = (XSSFRow)sheet.CreateRow(rowNum++);
                row.CreateCell(0).SetCellValue(empleado.NombreCompleto);
                row.CreateCell(1).SetCellValue(empleado.Correo);
                row.CreateCell(2).SetCellValue(empleado.Telefono);
                row.CreateCell(3).SetCellValue(empleado.oCargo?.Descripcion ?? "N/A");
            }

            //escribir el libro de trabajo en un MemoryStream
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            stream.Close();

            //regresar el archivo Excel como un archivo descargable
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tareas.xlsx");
        }

        public IActionResult Filtrar(int idCliente) {

            if (idCliente == 0) {
                var empleadosFiltrados1 = _DBcontext.Empleados
                .Include(e => e.oCargo)
                .ToList();

                return PartialView("_ListaEmpleadosPartial", empleadosFiltrados1);
            }
            var empleadosFiltrados = _DBcontext.Empleados
                .Where(e => e.IdEmpleado == idCliente)
                .Include(e => e.oCargo)
                .ToList();

            return PartialView("_ListaEmpleadosPartial", empleadosFiltrados);
        }


    }
}