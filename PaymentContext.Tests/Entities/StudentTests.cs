using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests() 
        {
            _name = new Name("Gabriel", "Cardoso");
            _document = new Document("70905632001", EDocumentType.CPF);
            _email = new Email("gabriel.cardoso@test.com");
            _address = new Address("Rua dahor", "123", "Bairro test", "Cidade feliz", "Sp", "Brasil", "12345678");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment(
                                "12345678", 
                                DateTime.Now, 
                                DateTime.Now.AddDays(5), 
                                10, 
                                10, 
                                "Gabriel", 
                                _document,
                                _address,
                                _email
                            );
            
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenActiveSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            Assert.IsTrue(_student.Invalid);
        }

        public void ShouldReturnSucessWhenAddSubscription()
        {
            var payment = new PayPalPayment(
                                "12345678", 
                                DateTime.Now, 
                                DateTime.Now.AddDays(5), 
                                10, 
                                10, 
                                "Gabriel", 
                                _document,
                                _address,
                                _email
                            );
            
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }
    }
}
