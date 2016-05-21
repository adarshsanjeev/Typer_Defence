#!/bin/python2.7

import os
from string import ascii_lowercase

SWEARFILE = "swear.txt"
swear_string = [ i.strip() for i in open(SWEARFILE, 'r').readlines() ]
FOLDER = "words/"

infile_name = raw_input("Choose a filename: ")
os.system('sort ' + infile_name + '| uniq > tempfile~')
os.rename('tempfile~', infile_name)

words = [ i.strip() for i in open(infile_name, 'r').readlines() ]

filtered_words = filter(lambda x: x not in swear_string, words)

class division:
    def __init__(self, label, lower, upper):
        self.label = label
        self.lower = lower
        self.upper = upper


divs = []
number = int(raw_input("Enter number of divisions: "))


for i in range(number): 
    label = raw_input("Enter label for division " + str(i) + ": ")
    low = int(raw_input("Enter least number of letters for division" + str(i) + ": "))
    high = int(raw_input("Enter most number of letters for division" + str(i) + ": "))
    
    divs += [division(label, low, high)]

for div in divs:
    for i in ascii_lowercase:
        open(FOLDER+div.label+"_"+i, 'w').write('[')

for word in filtered_words:
    for div in divs:
        if div.lower <= len(word) and len(word) <= div.upper:
            i = word[0]
            open(FOLDER+div.label+"_"+i, 'a+').write("'"+word+"',")
            
for div in divs:
    for i in ascii_lowercase:
        temp = open(FOLDER+div.label+"_"+i, 'a+')
        temp.seek(-1, 2)
        temp.truncate()
        temp.write(']')
