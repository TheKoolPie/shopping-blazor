function Get-CommitDetails {
    param (
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string] $CommitId
    )

    $commitDetails = git show --name-only --oneline $commitId
    $commitMessage, $filesInCommit = $commitDetails
    $commitMessage = $commitMessage.Replace($commitId, "").Trim()

    return $commitMessage, $filesInCommit
}
function Get-DistinctChangedFilesOfCommits {
    param (
        [Parameter(Mandatory = $true)]
        [string[]] $CommitIds
    )
    $changedFiles = New-Object Collections.Generic.List[string]

    foreach ($commitId in $CommitIds) {
        $commitMessage, $filesInCommit = Get-CommitDetails($commitId)

        foreach ($file in $filesInCommit) {
            if (!$changedFiles.Contains($file)) {
                $changedFiles.Add($file)
            }
        }
    }
    return $changedFiles
}

$branches = git branch -r

foreach ($branch in $branches) {
    $branch = $branch.Trim()
    $commitsInBranch = git log origin/main..$branch --oneline --pretty=format:"%h" --since=24.hour

    if ($commitsInBranch) {
        $changedFiles = Get-DistinctChangedFilesOfCommits $commitsInBranch
    }
}

