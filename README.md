# Phish API
see https://phishnet.api-docs.io/v3/the-phish-net-api/welcome
- NOTE: please do not use this library as a 'scraper' to persist this data to long term storage, as that is against the api terms of service
- From https://phishnet.api-docs.io/v3/the-phish-net-api 'Please note that scraping our site, including long term storage of our API responses, is against the API terms of service. Any apps found storing data beyond the grace period will be immediately suspended.'
# Projects

## Phish.ApiClient (.Net Standard 2.0)
- Wrapper library for Phish Rest Api
- Uses In Memory Cache with a 24 hour absolute expiration per the api guidelines
- From https://phishnet.api-docs.io/v3/the-phish-net-api 'We do not permit local storage of our data. We do permit temporary storage during your "grace period," which is 24 hours from the moment of request.'

## Phish.ApiClient.Tests
- Contains Integration Tests - live api calls

## Phish.Domain (.Net Standard 2.0)
- Contains Simple POCO objects that represents the return types from the Api

## Phish.WebApi (.Net Core 2.2)
- Rest Api Wrapper for Phish.ApiClient

## Phish.WebApi.Tests
- Contains Integration Tests to exercise the webapi