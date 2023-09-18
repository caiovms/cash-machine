using Cash.Machine.Application.Abstracts;
using Cash.Machine.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace Cash.Machine.WebApi.Controllers
{
    public class CashMachineController : Controller
    {
        private readonly IMovementApplicationService _movementApplicationService;
        private readonly IAccountApplicationService _accountApplicationService;
        private readonly IOperationApplicationService _operationApplicationService;

        private static readonly decimal rateProfitability = 0.01M;
        private static readonly CashMachineViewModel cashMachine = new CashMachineViewModel();
        private readonly ExtratoViewModel bankStatement = new ExtratoViewModel();

        public CashMachineController(IMovementApplicationService movementApplicationService,
                                     IAccountApplicationService accountApplicationService,
                                     IOperationApplicationService operationApplicationService)
        {
            _movementApplicationService = movementApplicationService;
            _accountApplicationService = accountApplicationService;
            _operationApplicationService = operationApplicationService;
        }

        public IActionResult Index()
        {
            Initialize();

            if (TempData["ErroMessage"] != null)
            {
                ViewBag.ErroMessage = TempData["ErroMessage"].ToString();
            }

            return View(cashMachine);
        }

        [HttpPost]
        public IActionResult Withdraw(WithdrawViewModel withdraw)
        {
            try
            {
                _movementApplicationService.Withdraw(withdraw.AccountId, withdraw.OperationId, withdraw.OperationAmount);
            }
            catch (Exception e)
            {
                TempData["ErroMessage"] = e.Message;
            }

            CleanFields();
            return RedirectToAction("Index", "CashMachine");
        }

        [HttpPost]
        public IActionResult Deposit(DepositViewModel deposit)
        {
            try
            {
                _movementApplicationService.Deposit(deposit.AccountId, deposit.OperationId, deposit.OperationAmount);
            }
            catch (Exception e)
            {
                TempData["ErroMessage"] = e.Message;
            }

            CleanFields();
            return RedirectToAction("Index", "CashMachine");
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel payment)
        {
            try
            {
                _movementApplicationService.Payment(payment.AccountId, payment.OperationId, payment.OperationAmount, payment.BarCode);
            }
            catch (Exception e)
            {
                TempData["ErroMessage"] = e.Message;
            }

            CleanFields();
            return RedirectToAction("Index", "CashMachine");
        }

        [HttpPost]
        public IActionResult Monetize(MonetizeViewModel monetize)
        {
            try
            {
                _movementApplicationService.Monetize(monetize.AccountId, monetize.OperationId, rateProfitability);
            }
            catch (Exception e)
            {
                TempData["ErroMessage"] = e.Message;
            }

            return RedirectToAction("Index", "CashMachine");
        }

        public IActionResult SetOperation(int operationId)
        {
            if (operationId == 0)
            {
                CleanFields();
                return RedirectToAction("Index", "CashMachine");
            }
            else

            {
                try
                {
                    cashMachine.OperationId = _operationApplicationService.Get(operationId).Id;
                    CleanFields();
                }
                catch (Exception e)
                {
                    TempData["ErroMessage"] = e.Message;
                }
                return RedirectToAction("Index", "CashMachine");
            }
        }

        private void Initialize()
        {
            try
            {
                var account = _accountApplicationService.Get(1);

                cashMachine.Account = account;
                cashMachine.BankStatement = bankStatement;
                cashMachine.BankStatement.Movements = account.Movements.ToList();
            }
            catch (Exception e)
            {
                TempData["ErroMessage"] = e.Message;
            }
        }
        
        private void CleanFields()
        {
            cashMachine.BarCode = null;
            cashMachine.OperationAmount = null;
        }
    }
}