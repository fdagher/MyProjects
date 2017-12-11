using LibGit2Sharp;
using System;
using System.IO;
using System.Linq;

namespace GitAdd
{
    public class GitRepositoryManager
    {
        private readonly string _repoSource;
        private readonly UsernamePasswordCredentials _credentials;
        private readonly DirectoryInfo _localFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GitRepositoryManager" /> class.
        /// </summary>
        /// <param name="username">The Git credentials username.</param>
        /// <param name="password">The Git credentials password.</param>
        /// <param name="gitRepoUrl">The Git repo URL.</param>
        /// <param name="localFolder">The full path to local folder.</param>
        public GitRepositoryManager(string username, string password, string gitRepoUrl, string localFolder)
        {
            var folder = new DirectoryInfo(localFolder);

            if (!folder.Exists)
            {
                throw new Exception(string.Format("Source folder '{0}' does not exist.", _localFolder));
            }

            _localFolder = folder;

            _credentials = new UsernamePasswordCredentials
            {
                Username = username,
                Password = password
            };

            _repoSource = gitRepoUrl;

            //Repository.Clone(_repoSource, localFolder);
        }

        /// <summary>
        /// Commits all changes.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.Exception"></exception>
        public bool CommitAllChanges(string message)
        {
            try
            {
                using (var repo = new Repository(_localFolder.FullName))
                {
                    var files = _localFolder.GetFiles("*", SearchOption.AllDirectories).Select(f => f.FullName);
                    repo.Stage(files);
                    //Commands.Stage(repo, _localFolder.FullName);

                    repo.Commit(message);
                }

                return true;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("nothing to commit"))
                {
                    return false;
                }
                throw;
            }
        }

        /// <summary>
        /// Pushes all commits.
        /// </summary>
        /// <param name="remoteName">Name of the remote server.</param>
        /// <param name="branchName">Name of the remote branch.</param>
        /// <exception cref="System.Exception"></exception>
        public void PushCommits(string remoteName, string branchName)
        {
            using (var repo = new Repository(_localFolder.FullName))
            {
                var remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == remoteName);
                if (remote == null)
                {
                    repo.Network.Remotes.Add(remoteName, _repoSource);
                    remote = repo.Network.Remotes.FirstOrDefault(r => r.Name == remoteName);
                }

                var options = new PushOptions
                {
                    CredentialsProvider = (url, usernameFromUrl, types) => _credentials
                };

                repo.Network.Push(remote, branchName, options);
            }
        }
    }
}
