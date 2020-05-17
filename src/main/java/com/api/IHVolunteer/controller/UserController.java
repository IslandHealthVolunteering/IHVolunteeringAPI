package com.api.IHVolunteer.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.api.IHVolunteer.model.User;

@RestController
public class UserController {

    private User testUser = new User("ahmed@example.com", "Ahmed", "Siddiqui", 123);

    @GetMapping("/user")
    public User getUser(@RequestParam(value = "email", required = true) String email) {
        return testUser;
    }
}
