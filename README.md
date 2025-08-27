# SE-PaymentGateway


# Payment Gateway - Software Abstraction Demonstration

## Project Overview

This project is a clean demonstration of software abstraction using the Payment Gateway scenario. It showcases how different payment methods can be made interchangeable through a common interface, allowing client code to remain unchanged when switching between payment providers.

## Problem Statement

**Requirement**: "Abstraction – Payment Gateway: Define an interface for processing payments so different methods are swappable."

## Project Architecture

### Core Abstraction
The project implements the Interface Segregation Principle and Dependency Inversion Principle through:

```csharp
public interface IPaymentGateway
{
    PaymentReceipt Pay(decimal amount, string currency);
}
```

### Implementation Strategy
- **Single Responsibility**: Each payment gateway handles one specific payment method
- **Open/Closed Principle**: New payment methods can be added without modifying existing code
- **Polymorphism**: All gateways can be used interchangeably through the common interface

## Implementation Details

### 1. Project Structure
```
PaymentGateway/
├── src/
│   └── PaymentGateway/
│       ├── Payment.cs           # Core interface and implementations
│       └── PaymentGateway.csproj
├── tests/
│   └── PaymentGateway.Tests/
│       ├── PaymentGatewayTests.cs
│       └── PaymentGateway.Tests.csproj
├── .editorconfig               # Coding standards
└── PaymentGateway.sln         # Solution file
```

### 2. Implemented Payment Gateways

| Gateway | Description | Provider Name |
|---------|-------------|---------------|
| `UpiGateway` | Unified Payments Interface | "UPI" |
| `CardGateway` | Credit/Debit Card payments | "Card" |
| `PayPalGateway` | PayPal online payments | "PayPal" |
| `NetBankingGateway` | Internet banking | "NetBanking" |

### 3. Common Behavior
All payment gateways implement consistent behavior:
- **Input Validation**: Ensures amount is positive
- **Currency Normalization**: Converts currency to uppercase
- **Unique Reference Generation**: Creates unique transaction IDs
- **Standardized Response**: Returns `PaymentReceipt` record

### 4. Data Models
```csharp
public record PaymentReceipt(string Provider, string ReferenceId, decimal Amount, string Currency);
```

## Abstraction Demonstration

### Before Abstraction (Tightly Coupled)
```csharp
// Client code depends on specific implementations
var upiPayment = new UpiGateway();
upiPayment.Pay(100.50m, "INR");

var cardPayment = new CardGateway();
cardPayment.Pay(100.50m, "INR");
```

### After Abstraction (Loosely Coupled)
```csharp
// Client code depends only on the interface
IPaymentGateway gateway = new UpiGateway();       // Easily swappable
PaymentReceipt receipt = gateway.Pay(100.50m, "INR");

// Switch payment method without changing client code
gateway = new PayPalGateway();                    // Just change this line
PaymentReceipt receipt2 = gateway.Pay(100.50m, "INR");
```

## Testing Strategy

### Unit Tests Coverage
- **Successful Payment Processing**: Validates correct receipt generation
- **Input Validation**: Tests negative amount handling
- **Currency Normalization**: Ensures consistent currency format
- **Reference ID Uniqueness**: Verifies unique transaction IDs
- **All Gateway Types**: Tests every payment method implementation

### Test Framework
- **MSTest**: Microsoft's official .NET testing framework
- **Structured Testing**: Arrange-Act-Assert pattern
- **Comprehensive Coverage**: Tests all public methods and edge cases

## Development Process

### Step 1: Project Setup
1. Created .NET solution with two projects:
   - **Class Library**: `PaymentGateway` (core implementation)
   - **Test Project**: `PaymentGateway.Tests` (unit tests)
2. Added `.editorconfig` for consistent coding standards

### Step 2: Interface Design
1. Defined clean, minimal `IPaymentGateway` interface
2. Created immutable `PaymentReceipt` record for responses
3. Established consistent method signature

### Step 3: Implementation
1. Implemented four different payment gateways
2. Ensured consistent validation and error handling
3. Added unique reference ID generation for each transaction

### Step 4: Testing
1. Wrote comprehensive unit tests for each gateway
2. Tested edge cases and error conditions
3. Verified polymorphic behavior works correctly

## Key Benefits Achieved

### 1. **Interchangeability**
Payment methods can be swapped without changing client code:
```csharp
IPaymentGateway gateway = GetPaymentGateway(userChoice);
PaymentReceipt receipt = gateway.Pay(amount, currency);
```

### 2. **Extensibility**
New payment methods can be added easily:
```csharp
public class CryptoGateway : IPaymentGateway
{
    public PaymentReceipt Pay(decimal amount, string currency)
    {
        // Implementation here
    }
}
```

### 3. **Testability**
Interface allows easy mocking and unit testing

### 4. **Maintainability**
Each payment method is isolated and can be modified independently

## Usage Example

```csharp
// Factory pattern for gateway selection
public static IPaymentGateway CreateGateway(PaymentMethod method)
{
    return method switch
    {
        PaymentMethod.UPI => new UpiGateway(),
        PaymentMethod.Card => new CardGateway(),
        PaymentMethod.PayPal => new PayPalGateway(),
        PaymentMethod.NetBanking => new NetBankingGateway(),
        _ => throw new ArgumentException("Invalid payment method")
    };
}

// Client usage
var gateway = CreateGateway(PaymentMethod.UPI);
var receipt = gateway.Pay(299.99m, "INR");
Console.WriteLine($"Payment processed via {receipt.Provider}: {receipt.ReferenceId}");
```

## Technical Specifications

- **Framework**: .NET 8.0
- **Language**: C# 12
- **Testing**: MSTest v3.2.0
- **Architecture Pattern**: Interface-based abstraction
- **Design Principles**: SOLID principles compliance

## Learning Outcomes

This project demonstrates:
- **Interface Segregation**: Clean, focused interface design
- **Polymorphism**: Runtime behavior switching through interfaces
- **Abstraction**: Hiding implementation details behind contracts
- **Extensibility**: Easy addition of new payment methods
- **Testing**: Comprehensive unit test coverage
- **Clean Code**: Following .NET coding standards

## Conclusion

This Payment Gateway project successfully demonstrates **software abstraction** by creating a flexible, extensible payment processing system. The abstraction allows different payment methods to be used interchangeably while maintaining consistent behavior and enabling easy testing and maintenance.

The implementation showcases real-world application of OOP principles and demonstrates how abstraction leads to more maintainable, flexible, and testable code.
