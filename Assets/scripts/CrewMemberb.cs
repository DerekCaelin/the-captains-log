using UnityEngine;
using System.Collections;
using System;

public class CrewMemberb: MonoBehaviour{

	public string rank;
	public string firstName;
	public string lastName;
	public string gender;
	public int age;

	public CrewMemberb (string newFirstName, string newLastName, string newRank,string newGender, int newAge){

		firstName = newFirstName;
		lastName = newLastName;
		rank = newRank;
		gender = newGender;
		age = newAge;
		//Debug.Log (firstName + lastName + rank + gender + age);

	}


}
