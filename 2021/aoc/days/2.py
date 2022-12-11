from typing import List


def run(data: List[str]) -> None:
    depth = 0
    depth_part2 = 0
    horizontal_pos = 0
    aim = 0

    for line in data:
        match line.split():
            case ["forward", amount]:
                horizontal_pos += int(amount)
                depth_part2 += aim * int(amount)
            case ["down", amount]:
                depth += int(amount)
                aim += int(amount)
            case ["up", amount]:
                depth -= int(amount)
                aim -= int(amount)

    print(
        f"Part 1: Horizontal={horizontal_pos}, depth={depth} => {depth*horizontal_pos}"
    )

    print(
        f"Part 2: Horizontal={horizontal_pos}, depth={depth_part2}, aim={aim} => {depth_part2*horizontal_pos}"
    )
