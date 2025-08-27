using System;

namespace PaymentGatewayAbstraction;

public interface IPaymentGateway
{
    PaymentReceipt Pay(decimal amount, string currency);
}

public sealed record PaymentReceipt(string Provider, decimal Amount, string Currency, string ReferenceId)
{
    public override string ToString() => $"{Provider}:{ReferenceId} {Amount} {Currency}";
}

public sealed class UpiGateway : IPaymentGateway
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        var refId = $"UPI-{Guid.NewGuid():N}".ToUpperInvariant();
        return new PaymentReceipt("UPI", decimal.Round(amount, 2), currency.ToUpperInvariant(), refId);
    }
}

public sealed class CardGateway : IPaymentGateway
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        var refId = $"CARD-{Guid.NewGuid():N}".ToUpperInvariant();
        return new PaymentReceipt("CARD", decimal.Round(amount, 2), currency.ToUpperInvariant(), refId);
    }
}

public sealed class PayPalGateway : IPaymentGateway
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        var refId = $"PAYPAL-{Guid.NewGuid():N}".ToUpperInvariant();
        return new PaymentReceipt("PAYPAL", decimal.Round(amount, 2), currency.ToUpperInvariant(), refId);
    }
}

public sealed class NetBankingGateway : IPaymentGateway
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        var refId = $"NETBANK-{Guid.NewGuid():N}".ToUpperInvariant();
        return new PaymentReceipt("NETBANKING", decimal.Round(amount, 2), currency.ToUpperInvariant(), refId);
    }
}
