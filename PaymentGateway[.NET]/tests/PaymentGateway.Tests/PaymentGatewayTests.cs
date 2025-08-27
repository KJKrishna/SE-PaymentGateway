using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentGatewayAbstraction;

namespace PaymentGateway.Tests;

[TestClass]
public class PaymentGatewayTests
{
    [DataTestMethod]
    [DataRow(499.99, "INR")]
    [DataRow(1, "usd")]
    public void UpiGateway_Should_ReturnReceipt(double amount, string currency)
    {
        IPaymentGateway gw = new UpiGateway();
        var receipt = gw.Pay((decimal)amount, currency);

        receipt.Provider.Should().Be("UPI");
        receipt.Amount.Should().Be(Math.Round((decimal)amount, 2));
        receipt.Currency.Should().Be(currency.ToUpperInvariant());
        receipt.ReferenceId.Should().StartWith("UPI-");
    }

    [TestMethod]
    public void CardGateway_Should_ReturnReceipt()
    {
        IPaymentGateway gw = new CardGateway();
        var receipt = gw.Pay(250m, "EUR");

        receipt.Provider.Should().Be("CARD");
        receipt.ReferenceId.Should().StartWith("CARD-");
    }

    [DataTestMethod]
    [DataRow(0)]
    [DataRow(-10)]
    public void Gateways_Should_Reject_NonPositive(int bad)
    {
        IPaymentGateway upi = new UpiGateway();
        IPaymentGateway card = new CardGateway();

        Action a = () => upi.Pay((decimal)bad, "INR");
        Action b = () => card.Pay((decimal)bad, "INR");

        a.Should().Throw<ArgumentOutOfRangeException>();
        b.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestMethod]
    public void PayPalGateway_Should_ReturnReceipt()
    {
        IPaymentGateway gw = new PayPalGateway();
        var receipt = gw.Pay(150.75m, "usd");

        receipt.Provider.Should().Be("PAYPAL");
        receipt.Currency.Should().Be("USD");
        receipt.ReferenceId.Should().StartWith("PAYPAL-");
    }

    [TestMethod]
    public void NetBankingGateway_Should_ReturnReceipt()
    {
        IPaymentGateway gw = new NetBankingGateway();
        var receipt = gw.Pay(999.50m, "inr");

        receipt.Provider.Should().Be("NETBANKING");
        receipt.Currency.Should().Be("INR");
        receipt.ReferenceId.Should().StartWith("NETBANK-");
    }
}
