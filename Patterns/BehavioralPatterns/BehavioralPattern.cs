using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.BehavioralPatterns
{
    public static class BehavioralPattern
    {
        public static void Run() 
        {
            //Chain of Responsibility is a behavioral design pattern that lets you pass requests along a chain of handlers.
            //Upon receiving a request, each handler decides either to process the request or to pass it to the next handler in the chain.
            ChainOfResponsibilityExecute();

            //Command is a behavioral design pattern that turns a request into a stand - alone object that contains all information about the request.
            //This transformation lets you pass requests as a method arguments, delay or queue a request’s execution, and support undoable operations.
            CommandExecute();

            //Iterator is a behavioral design pattern that lets you traverse elements of a collection without exposing its
            //underlying representation (list, stack, tree, etc.).
            IteratorExecute();

            //Mediator is a behavioral design pattern that lets you reduce chaotic dependencies between objects.
            //The pattern restricts direct communications between the objects and forces them to collaborate only via a mediator object.
            MediatorExecute();

            //Memento is a behavioral design pattern that lets you save and restore the previous state of an object
            //without revealing the details of its implementation.
            MementoExecute();

            //Observer is a behavioral design pattern that lets you define a subscription mechanism to notify multiple objects
            //about any events that happen to the object they’re observing.
            ObserverExecute();

            //State is a behavioral design pattern that lets an object alter its behavior when its internal state changes.
            //It appears as if the object changed its class.
            StateExecute();

            //Strategy is a behavioral design pattern that lets you define a family of algorithms, put each of them into a separate class,
            //and make their objects interchangeable.
            StrategyExecute();

            //Template Method is a behavioral design pattern that defines the skeleton of an algorithm in the superclass but lets subclasses override specific steps
            //of the algorithm without changing its structure.
            TemplateMethodExecute();

            //Visitor is a behavioral design pattern that lets you separate algorithms from the objects on which they operate.
            VisitorExecute();
        }

        private static void ChainOfResponsibilityExecute()
        {

        }
        private static void CommandExecute()
        {

        }
        private static void IteratorExecute()
        {

        }
        private static void MediatorExecute()
        {

        }
        private static void MementoExecute()
        {

        }
        private static void ObserverExecute()
        {

        }
        private static void StateExecute()
        {

        }
        private static void StrategyExecute()
        {

        }
        private static void TemplateMethodExecute()
        {

        }
        private static void VisitorExecute()
        {

        }
    }
}
