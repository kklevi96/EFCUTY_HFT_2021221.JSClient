fetch('http://localhost:54726/citizen')
    .then(x => x.json())
    .then(y => console.log(y));