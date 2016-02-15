using UnityEngine;
using System.Collections;
using System;

public class CrewMember{

	public int number;
	public string rank;
	public string firstName;
	public string lastName;
	public string gender;
	public int age;
	public string possessive;
	public string pronoun;
	public string diro;

	public CrewMember (int newNumber, string newFirstName, string newLastName, string newRank,string newGender, int newAge, string newPossessive, string newPronoun, string newDiro){

		number = newNumber;
		firstName = newFirstName;
		lastName = newLastName;
		rank = newRank;
		gender = newGender;
		age = newAge;
		possessive = newPossessive;
		pronoun = newPronoun;
		diro = newDiro;
		//Debug.Log (firstName + lastName + rank + gender + age);

	}


}
