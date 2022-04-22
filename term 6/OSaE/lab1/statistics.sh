#!/bin/bash
current_time=$(date +"%T")
date=$(date +"%d-%m-%Y")
processes_count=$(ps -e | wc -l)
running_time=$(uptime -p)
echo "Username:$USER
Time: $current_time, date: $date
Current location: $PWD
Count of processes : $processes_count
Time of running system : $running_time" > output.txt
