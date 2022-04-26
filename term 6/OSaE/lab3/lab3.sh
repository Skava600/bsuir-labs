#!/bin/bash

inputFile="input.csv"
tableName=$(sed '1q;d' $inputFile)
tableColumns=($(sed '2q;d' $inputFile))

outputFile="output.sql"
> $outputFile
sed 1,2d $inputFile | while read line
do
  echo "INSERT INTO $tableName(${tableColumns[*]}) VALUES($line);" >> $outputFile
done