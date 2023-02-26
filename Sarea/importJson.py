import json
import os

# Open the input file
with open(os.path.join(os.path.dirname(os.path.abspath(__file__)), '/Users/tommy/source/repos/Sarea/Sarea/bin/Debug/net6.0/Data/Arknights/CharacterTable.json'), encoding='utf-8') as f_in:
    # Load the input file as a JSON object
    data = json.load(f_in)

    # Loop through each item in the JSON object
    for item in data:
        # Check if the item starts with "char_"
        if item.startswith("char_"):
            # Create a new file with a name based on the item's key
            file_name = item + ".json"
            with open(file_name, 'w') as f_out:
                # Write the item to the new file
                json.dump(data[item], f_out, indent=4)