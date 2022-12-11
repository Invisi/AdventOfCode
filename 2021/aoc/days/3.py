from dataclasses import dataclass, field
from typing import Callable, Literal


def run(data: list[str]) -> None:
    counter = count_common(data)

    gamma = ""
    epsilon = ""
    for i, values in enumerate(counter):
        gamma += values.most_common
        epsilon += values.least_common

    # Oxygen generator rating
    valid_matches: list[str] = data.copy()
    oxy_counter = find_match(data, lambda line, counter: line == counter.most_common)
    co2_counter = find_match(data, lambda line, counter: line == counter.least_common)

    print("Part 1:")
    print(f"Gamma: {gamma} => {int(gamma, 2)}")
    print(f"Epsilon: {epsilon} => {int(epsilon, 2)}")
    print(f"{int(gamma, 2)*int(epsilon, 2)}")

    print("\nPart 2:")
    print(f"Oxygen generator rating: {oxy_counter} => {int(oxy_counter, 2)}")
    print(f"CO₂ scrubber rating: {co2_counter} => {int(co2_counter, 2)}")
    print(f"{int(oxy_counter, 2)*int(co2_counter, 2)}")


@dataclass
class IndexCounter:
    raw_data: dict[Literal["0", "1"], int] = field(default_factory=dict)

    @property
    def most_common(self) -> str:
        if self.raw_data["0"] > self.raw_data["1"]:
            return "0"
        return "1"

    @property
    def least_common(self) -> str:
        if self.raw_data["0"] > self.raw_data["1"]:
            return "1"
        return "0"


def count_common(data: list[str]) -> list[IndexCounter]:
    counter: list[IndexCounter] = []

    for i in data[0]:
        counter.append(IndexCounter())

    for line in data:
        for i, character in enumerate(line):
            if character not in counter[i].raw_data:
                counter[i].raw_data[character] = 1
            else:
                counter[i].raw_data[character] += 1

    return counter


def find_match(data: list[str], check: Callable[[str, IndexCounter], bool]) -> str:
    valid_matches = data.copy()
    for i in range(len(data[0])):
        counter = count_common(valid_matches)
        matches = []
        for line in valid_matches:
            if check(line[i], counter[i]):
                matches.append(line)

        if len(matches) == 1:
            return matches[0]

        valid_matches = matches
