function substring(string, startIndex , count) {
    'use strict'
    let result = string.substring(startIndex, startIndex + count);

    console.log(result);
}

substring('ASentence', 1, 8)
substring('SkipWord', 4, 7)
