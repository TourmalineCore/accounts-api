Feature: Test Flow
# https://github.com/karatelabs/karate/issues/1191
# https://github.com/karatelabs/karate?tab=readme-ov-file#karate-fork

Background:
* url 'http://localhost:1080/mockServer/verify'
* header Content-Type = 'application/json'

Scenario: CRUD operations test flow

    * def jsUtils = read('jsUtils.js')
    * def authApiRootUrl = jsUtils().getEnvVariable('AUTH_API_ROOT_URL')
    * def apiRootUrl = jsUtils().getEnvVariable('API_ROOT_URL')
    * def authLogin = jsUtils().getEnvVariable('AUTH_LOGIN')
    * def authPassword = jsUtils().getEnvVariable('AUTH_PASSWORD')

    # Authentication
    Given url authApiRootUrl
    And path '/auth/login'
    And request {"login": #(authLogin), "password": #(authPassword)}
    And method POST
    Then status 200

    * def accessToken = karate.toMap(response.accessToken.value)

    * def accessTokenForExternalDeps = karate.toMap('Bearer ' + response.accessToken.value)

    * configure headers = jsUtils().getAuthHeaders(accessToken)

    Given url apiRootUrl
    Given path '/api/tenants'
    * def tenantName = 'Test tenant' + Math.random()
    And request { name: '#(tenantName)' }
    When method post
    Then status 200
    * def tenantId = response

    Given path '/api/roles/create'
    * def roleName = 'Test role' + Math.random()
    And request { name: '#(roleName)', permissions: ["ViewAccounts"] }
    When method post
    Then status 200
    
    Given path '/api/accounts/create'
    * def firstName = 'test-' + Math.random()
    * def lastName = 'test-' + Math.random()
    * def middleName = 'test-' + Math.random()
    * def corporateEmail = 'test-' + java.util.UUID.randomUUID().toString() + '@tourmalinecore.com'
    And request { firstName: '#(firstName)', lastName: '#(lastName)', middleName: '#(middleName)', corporateEmail: '#(corporateEmail)', roleIds: [ 5 ], tenantId: '#(tenantId)' }
    And header Authorization = accessTokenForExternalDeps
    When method post
    Then status 200
    * def accountId = response

    Given path '/api/accounts/findById/' + accountId
    And header Authorization = accessTokenForExternalDeps
    When method get
    Then status 200
    * def ResponseTenantId = response.tenantId.toString()
    * match ResponseTenantId == tenantId
    * match response.roles[0].id == 5

    Given path '/api/accounts/delete-account'
    And header Authorization = accessTokenForExternalDeps
    And request { corporateEmail: '#(corporateEmail)'}
    When method post
    Then status 200

    Given path '/api/roles/5'
    When method delete
    Then status 200

    Given path '/api/tenants/' + tenantId
    When method delete
    Then status 200
