function test(){
    console.log("hello everyone!!");
}
function test2(num1, num2){
    return (num1 + num2);
}

test();
let sum = test2(2,5);
console.log(sum);

// using arrow functions
const testme = () => console.log("Hello world");
let sum2 = (n1,n2) => (n1 + n2);
testme();
console.log(sum2(12,45));

// Map function
//variable.map((element)=>print(element))
var arr = [10,20,30,40,50];
arr.map((ele)=>console.log(ele));
//example2
const numbers = [1,2,3,4,5];
const squares = numbers.map(value=>value*value);
console.log(squares);
//example3
const people = [{id: 1, name: "felpie", country: "USA"},
                {id: 2, name: "rashi", country: "INDIA"},
                {id: 3, name: "malinga", country: "SRI LANKA"}]
const ids = people.map(p=>p.id);
const names = people.map(p=>p.name);
const countries = people.map(p=>p.country);
console.log(ids);
console.log(names);
console.log(countries);

// Filter function
// array.filter(ele=>(condition))
var filtered = numbers.filter(x=>x>3);
console.log(filtered); 



