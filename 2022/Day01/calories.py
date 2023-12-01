import fileinput

class Elve(object):
    elfNo = -1
    totalCalories = 0

    def __init__(self, elfNo):
        self.elfNo = elfNo

    def addCalories(self, cals):
        self.totalCalories += cals

def LineToInt(line):
    return int(line.replace("\n", ""))

elfList = []
i = 0
noElf = 1

with fileinput.input(files=('input.txt'), encoding="utf-8") as f:
    for line in f:
        if i == 0:
            elf = Elve(noElf)
            elfList.append(elf)
            elfList[noElf-1].addCalories(LineToInt(line))
            i+=1
        else:
            if line == '\n':
                noElf+=1
                elf = Elve(noElf)
                elfList.append(elf)
            else:
                elfList[noElf-1].addCalories(LineToInt(line))

# elf avec le + de calories et son numéro.
maxVal = max(e.totalCalories for e in elfList)
print('max cals = ' + str(maxVal))
indexMaxVal = [k for k, l in enumerate(elfList) if l.totalCalories == maxVal]
elfNoMax = elfList[indexMaxVal[0]].elfNo
print('num elf max = ' + str(elfNoMax))

orderedList = sorted(elfList, key=lambda e: e.totalCalories, reverse=True)[:3]

print('top 3')

for elf in orderedList:
    print('No elf : ' + str(elf.elfNo))
    print('Nb cals : ' + str(elf.totalCalories))

totalTopTroisCal = sum(ol.totalCalories for ol in orderedList)

print('total cal top 3 : ' + str(totalTopTroisCal))
