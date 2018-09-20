# SpearSoft.RulesEngine  
##### Rules Engine

##### Note - SpearSoft.RulesEngine is a fork of the original RulesEngine that was published by athoma13 on CodePlex but has not been maintained for some time and was not ported (as far as I can tell) to another public source management system before CodePlex was shutdown.  The original but archived source can be found [here](https://archive.codeplex.com/?p=rulesengine).  I also changed the namespace on this version from RulesEngine to SpearSoft.RulesEngine to prevent namespace collisions.  

Rules Engine is a C# project that makes it easier for developers to define business rules on domain objects without coupling the domain object to the business rule. The rules engine supports cross-field validation and conditional validation. Rules are interface-based and are easily extensible. Rules can be added using a fluent interface.

## What is Rules Engine
Rules Engine is a . NET C# project that validates business logic by defining a bunch of rules for your data classes. Rules are defined using a fluent-interface (fluent validation) helper class, and not by decorating your existing objects with attributes, therefore de-coupling validation logic from data (or domain) classes.
##Features
  * __Rules are inheritable.__ Defining RuleX for TypeA will also apply to TypeB (given that TypeB inherits from Type A) Details.
  * __Rules are extensible.__ Creating custom rules are only a matter of implementing an interface. Details
  * __Conditional validation.__ When defining rules, it is possible to have different rules given different conditions. E.g. Not Null Rule only applies to FieldA when FieldB > 100. Details
  * __Cross-Field validation.__ Defining a rule that FieldA must be greater than FieldB comes very naturally with the fluent-interface helper class. Details
  * __Fluent-Interface.__ Adding rules to objects is done by a fluent-interface helper class. Details
  * __Error Messages.__ Defining error messages can be done at the same time as defining your rules (Or separately). They can apply to the Class, A Property of that class, or a Rule for that property. Details
## ASP.NET Mvc
An asp.net Mvc bridge is available for download. To use, create an instance of MvcValidationReport in one of your controllers, passing it your ModelState, Validate the model and that's it. The report will populate ModelState with error messages (if any). No more Attribute-Style validation.

NOTE: This does not support client-side validation.