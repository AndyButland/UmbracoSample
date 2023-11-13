# Umbraco Sample Site

This repository contains a very simple, server-side rendered, Umbraco website, implementing some patterns that I'd opinionatedly describe as best practices.

It's clearly over-engineered for what it is, but the idea was to build something that has a few general requirements we might need on many websites, and implement those in the best practice way. If it were ever extended to something more complicated, where the patterns have more value, they would already be in place.

Specifically I'm looking to demonstrate:

- Use of strongly typed, page specific view models for each page.
    - This means each page has it's own **view model**, specifically designed to represent the content of the page.
    - These aren't the models builder **content models**. For simple pages, there may be a one to one map between the content and view model. But that won't always be the case. Often we need to display information from other pages, or from other sources, on a single website page.
- Use of route hijacking and custom controllers for rendering all pages.
- Keeping the controllers slim by delegating to a generic **view model builder** or **view model decorator**.
- That the core logic of the web site can be unit tested.

In the sample, there are examples of:

- Creating view models in view mode builders, by mapping models builder content models to the view model properties.
- Extending the view model by retrieving information from other pages (e.g. the "most recent articles" on the blog landing page).
- Augmenting view models with common page properties - e.g. SEO information - via an attribute and a view model decorator.
- Demonstrating an asynchronous view model builder, where we retrieve information from an illustrative asynchronous service (e.g. the "weather service").
- Unit tests for all view model builders and decorators (which is where the majority of website logic resides).
