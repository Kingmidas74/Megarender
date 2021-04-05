Feature: Check authorization in the system
  
  Scenario: Free status without authorization successfully
    When User doesn't have authorization 
    Then Free status is available

  Scenario: Protect status without authorization failed
    When User doesn't have authorization
    Then Protect status is not available

  Scenario: Protect status with authorization successfully
    When User have authorization
    Then Protect status is available